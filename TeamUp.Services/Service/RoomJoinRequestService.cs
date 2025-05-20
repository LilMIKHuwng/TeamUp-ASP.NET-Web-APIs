using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BabyCare.Core.Utils.SystemConstant;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.Contract.Repositories.Interface;
using TeamUp.Contract.Services.Interface;
using TeamUp.Core.APIResponse;
using TeamUp.Core;
using TeamUp.ModelViews.RoomJoinRequestModelViews;
using Microsoft.EntityFrameworkCore;
using TeamUp.Repositories.Entity;
using BabyCare.Core.Utils;
using TeamUp.ModelViews.CourtModelViews;
using TeamUp.ModelViews.UserModelViews.Response;
using TeamUp.ModelViews.RoomModelViews;
using Azure.Core;
using TeamUp.ModelViews.SportsComplexModelViews;
using TeamUp.Core.Utils;

namespace TeamUp.Services.Service
{
    public class RoomJoinRequestService : IRoomJoinRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public RoomJoinRequestService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<ApiResult<BasePaginatedList<RoomJoinRequestModelView>>> GetAllRoomJoinRequestAsync(int pageNumber, int pageSize, int? requestId, int? roomId, string? status)
        {
            var query = _unitOfWork.GetRepository<RoomJoinRequest>().Entities
                .Include(r => r.Room)
                .Include(r => r.Requester)
                .Where(r => !r.DeletedTime.HasValue);

            if (requestId.HasValue)
                query = query.Where(r => r.RequesterId == requestId.Value);

            if (roomId.HasValue)
                query = query.Where(r => r.RoomId == roomId.Value);

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(r => r.Status == status);

            int totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(r => r.RequestedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var mapped = _mapper.Map<List<RoomJoinRequestModelView>>(items);

            for (int i = 0; i < mapped.Count; i++)
            {
                mapped[i].Room = _mapper.Map<RoomModelView>(items[i].Room);

                mapped[i].Room.Court = _mapper.Map<CourtModelView>(items[i].Room.Court);

                mapped[i].Room.Court.SportsComplexModelView = _mapper.Map<SportsComplexModelView>(items[i].Room.Court.SportsComplex);

                mapped[i].Requester = _mapper.Map<UserResponseModel>(items[i].Requester);
            }

            return new ApiSuccessResult<BasePaginatedList<RoomJoinRequestModelView>>(
                new BasePaginatedList<RoomJoinRequestModelView>(mapped, totalCount, pageNumber, pageSize));
        }

        public async Task<ApiResult<object>> AddRoomJoinRequestAsync(CreateRoomJoinRequestModelView model)
        {
            var roomRepo = _unitOfWork.GetRepository<Room>();
            var userRepo = _unitOfWork.GetRepository<ApplicationUser>();

            var lastRequest = await _unitOfWork.GetRepository<RoomJoinRequest>().Entities
                .Where(r => r.RoomId == model.RoomId &&
                            r.RequesterId == model.RequesterId &&
                            !r.DeletedTime.HasValue)
                .OrderByDescending(r => r.RequestedAt)
                .FirstOrDefaultAsync();

            if (lastRequest != null)
            {
                switch (lastRequest.Status)
                {
                    case RoomJoinRequestStatus.Pending:
                        return new ApiErrorResult<object>("Bạn đã gửi yêu cầu và đang chờ phê duyệt.");
                    case RoomJoinRequestStatus.Accepted:
                        return new ApiErrorResult<object>("Bạn đã được chấp nhận vào phòng này.");
                    case RoomJoinRequestStatus.Rejected:
                        return new ApiErrorResult<object>("Yêu cầu của bạn đã bị từ chối. Bạn không thể gửi lại yêu cầu.");
                    case RoomJoinRequestStatus.Cancelled:
                        // Cho phép tạo lại yêu cầu
                        break;
                }
            }


            var room = await roomRepo.Entities
                .Include(r => r.Host) // để lấy email Host
                .FirstOrDefaultAsync(r => r.Id == model.RoomId && !r.DeletedTime.HasValue);

            var requester = await userRepo.GetByIdAsync(model.RequesterId);

            if (room == null)
                return new ApiErrorResult<object>("Phòng không tồn tại.");
            if (requester == null)
                return new ApiErrorResult<object>("Người gửi yêu cầu không tồn tại.");

            if (room.HostId == model.RequesterId)
                return new ApiErrorResult<object>("Chủ phòng không thể gửi yêu cầu tham gia phòng của chính mình.");

            // Kiểm tra nếu đã gửi yêu cầu chưa được xử lý
            var existingRequest = await _unitOfWork.GetRepository<RoomJoinRequest>().Entities
                .FirstOrDefaultAsync(r =>
                    r.RoomId == model.RoomId &&
                    r.RequesterId == model.RequesterId &&
                    r.Status == RoomJoinRequestStatus.Pending &&
                    !r.DeletedTime.HasValue);

            if (existingRequest != null)
                return new ApiErrorResult<object>("Bạn đã gửi yêu cầu và đang chờ phê duyệt.");

            // Tạo yêu cầu mới
            var newRequest = _mapper.Map<RoomJoinRequest>(model);
            newRequest.Status = RoomJoinRequestStatus.Pending;
            newRequest.RequestedAt = DateTime.Now;
            newRequest.CreatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            await _unitOfWork.GetRepository<RoomJoinRequest>().InsertAsync(newRequest);
            await _unitOfWork.SaveAsync();

            // Gửi email cho chủ phòng
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormSendEmail", "NotifyHostRoomJoinRequest.html");
            path = Path.GetFullPath(path);
            if (!File.Exists(path))
                return new ApiErrorResult<object>("Không tìm thấy file gửi mail cho host.");

            var content = File.ReadAllText(path);
            content = content.Replace("{{HostName}}", room.Host.Email); // hoặc có thể dùng Name nếu bạn lưu tên
            content = content.Replace("{{RequesterEmail}}", requester.Email);
            content = content.Replace("{{RoomTitle}}", room.Name);

            var resultSendMail = DoingMail.SendMail(
                "TeamUp",
                "Yêu cầu tham gia phòng mới",
                content,
                room.Host.Email);

            if (!resultSendMail)
                return new ApiErrorResult<object>("Gửi email thông báo đến chủ phòng thất bại.");

            return new ApiSuccessResult<object>("Yêu cầu tham gia đã được gửi.");
        }



        public async Task<ApiResult<object>> UpdateRoomJoinRequestAsync(int id, UpdateRoomJoinRequestModelView model)
        {
            var repo = _unitOfWork.GetRepository<RoomJoinRequest>();
            var request = await repo.Entities.FirstOrDefaultAsync(r => r.Id == id && !r.DeletedTime.HasValue);

            if (request == null)
                return new ApiErrorResult<object>("Yêu cầu không tồn tại.");

            // Gán tạm để kiểm tra sau
            int roomId = model.RoomId ?? request.RoomId;
            int requesterId = model.RequesterId ?? request.RequesterId;

            // Lấy lại thông tin phòng để kiểm tra chủ phòng
            var room = await _unitOfWork.GetRepository<Room>().GetByIdAsync(roomId);
            if (room == null)
                return new ApiErrorResult<object>("Phòng không tồn tại.");

            if (room.HostId == requesterId)
                return new ApiErrorResult<object>("Chủ phòng không thể gửi yêu cầu tham gia phòng của chính mình.");

            // Cập nhật các thuộc tính
            if (model.RoomId.HasValue)
                request.RoomId = model.RoomId.Value;

            if (model.RequesterId.HasValue)
                request.RequesterId = model.RequesterId.Value;

            if (!string.IsNullOrWhiteSpace(model.Status))
            {
                var validStatuses = new[]
                {
                    SystemConstant.RoomJoinRequestStatus.Pending,
                    SystemConstant.RoomJoinRequestStatus.Accepted,
                    SystemConstant.RoomJoinRequestStatus.Rejected,
                    SystemConstant.RoomJoinRequestStatus.Cancelled
                };

                if (!validStatuses.Contains(model.Status))
                    return new ApiErrorResult<object>("Trạng thái không hợp lệ.");

                request.Status = model.Status;

                if (model.Status == SystemConstant.RoomJoinRequestStatus.Accepted)
                {
                    request.RespondedAt = DateTime.Now;
                }
            }

            request.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");
            request.LastUpdatedTime = DateTime.Now;

            await repo.UpdateAsync(request);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Cập nhật yêu cầu thành công.");
        }



        public async Task<ApiResult<object>> DeleteRoomJoinRequestAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<RoomJoinRequest>();
            var request = await repo.Entities.FirstOrDefaultAsync(r => r.Id == id && !r.DeletedTime.HasValue);

            if (request == null)
                return new ApiErrorResult<object>("Không tìm thấy yêu cầu.");

            request.DeletedTime = DateTime.Now;
            request.DeletedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            request.Status = SystemConstant.RoomJoinRequestStatus.Cancelled;

            await repo.UpdateAsync(request);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Xóa yêu cầu thành công.");
        }

        public async Task<ApiResult<RoomJoinRequestModelView>> GetRoomJoinRequestByIdAsync(int id)
        {
            var request = await _unitOfWork.GetRepository<RoomJoinRequest>().Entities
                .Include(r => r.Room)
                .Include(r => r.Requester)
                .FirstOrDefaultAsync(r => r.Id == id && !r.DeletedTime.HasValue);

            if (request == null)
                return new ApiErrorResult<RoomJoinRequestModelView>("Không tìm thấy yêu cầu.");

            var result = _mapper.Map<RoomJoinRequestModelView>(request);

            result.Room = _mapper.Map<RoomModelView>(request.Room);

            result.Room.Court = _mapper.Map<CourtModelView>(request.Room.Court);

            result.Room.Court.SportsComplexModelView = _mapper.Map<SportsComplexModelView>(request.Room.Court.SportsComplex);

            result.Requester = _mapper.Map<UserResponseModel>(request.Requester);

            return new ApiSuccessResult<RoomJoinRequestModelView>(result);
        }

        public async Task<ApiResult<List<RoomJoinRequestModelView>>> GetAllRoomJoinRequest()
        {
            var requests = await _unitOfWork.GetRepository<RoomJoinRequest>().Entities
                .Include(r => r.Room)
                .Include(r => r.Requester)
                .OrderByDescending(cb => cb.RequestedAt)
                .Where(r => !r.DeletedTime.HasValue)
                .ToListAsync();

            var result = _mapper.Map<List<RoomJoinRequestModelView>>(requests);

            for (int i = 0; i < result.Count; i++)
            {
                result[i].Room = _mapper.Map<RoomModelView>(requests[i].Room);

                result[i].Room.Court = _mapper.Map<CourtModelView>(requests[i].Room.Court);

                result[i].Room.Court.SportsComplexModelView = _mapper.Map<SportsComplexModelView>(requests[i].Room.Court.SportsComplex);

                result[i].Requester = _mapper.Map<UserResponseModel>(requests[i].Requester);
            }

            return new ApiSuccessResult<List<RoomJoinRequestModelView>>(result);
        }

        public async Task<ApiResult<object>> UpdateRoomJoinRequestStatusAsync(int id, string status)
        {
            var repo = _unitOfWork.GetRepository<RoomJoinRequest>();
            var request = await repo.Entities
                .Include(r => r.Room)
                .Include(r => r.Requester)
                .FirstOrDefaultAsync(r => r.Id == id && !r.DeletedTime.HasValue);

            if (request == null)
                return new ApiErrorResult<object>("Yêu cầu tham gia không tồn tại.");

            var validStatuses = new[]
            {
                SystemConstant.RoomJoinRequestStatus.Pending,
                SystemConstant.RoomJoinRequestStatus.Accepted,
                SystemConstant.RoomJoinRequestStatus.Rejected,
                SystemConstant.RoomJoinRequestStatus.Cancelled
            };

            if (!validStatuses.Contains(status))
                return new ApiErrorResult<object>("Trạng thái không hợp lệ.");

            if (status == SystemConstant.RoomJoinRequestStatus.Accepted)
            {
                // Kiểm tra số lượng người đã tham gia phòng
                var acceptedPlayerCount = await _unitOfWork.GetRepository<RoomPlayer>().Entities
                    .CountAsync(rp =>
                        rp.RoomId == request.RoomId &&
                        rp.Status == SystemConstant.RoomPlayerStatus.Accepted &&
                        !rp.DeletedTime.HasValue);

                if (acceptedPlayerCount >= request.Room.MaxPlayers)
                    return new ApiErrorResult<object>("Phòng đã đủ người, không thể chấp nhận thêm người chơi.");

                // Kiểm tra người chơi đã có trong phòng chưa
                var existingPlayer = await _unitOfWork.GetRepository<RoomPlayer>().Entities
                    .FirstOrDefaultAsync(rp =>
                        rp.RoomId == request.RoomId &&
                        rp.PlayerId == request.RequesterId &&
                        !rp.DeletedTime.HasValue);

                if (existingPlayer == null)
                {
                    var newRoomPlayer = new RoomPlayer
                    {
                        RoomId = request.RoomId,
                        PlayerId = request.RequesterId,
                        JoinedAt = DateTime.Now,
                        Status = SystemConstant.RoomPlayerStatus.Accepted,
                        CreatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0"),
                        CreatedTime = DateTime.Now,
                        IsNotified = false,
                    };

                    await _unitOfWork.GetRepository<RoomPlayer>().InsertAsync(newRoomPlayer);
                }

                request.RespondedAt = DateTime.Now;
            }

            request.Status = status;
            request.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");
            request.LastUpdatedTime = DateTime.Now;

            await repo.UpdateAsync(request);
            await _unitOfWork.SaveAsync();

            // Gửi email thông báo
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormSendEmail", "RoomJoinRequestStatus.html");
            path = Path.GetFullPath(path);
            if (!File.Exists(path))
                return new ApiErrorResult<object>("Không tìm thấy file gửi mail");

            var content = File.ReadAllText(path);
            content = content.Replace("{{Name}}", request.Requester.Email);

            string roleMessage = status switch
            {
                SystemConstant.RoomJoinRequestStatus.Accepted => $"Yêu cầu tham gia phòng <strong>{request.Room.Name}</strong> của bạn đã được <span style='color:green;font-weight:bold'>CHẤP NHẬN</span>.",
                SystemConstant.RoomJoinRequestStatus.Rejected => $"Yêu cầu tham gia phòng <strong>{request.Room.Name}</strong> của bạn đã bị <span style='color:red;font-weight:bold'>TỪ CHỐI</span>.",
                SystemConstant.RoomJoinRequestStatus.Cancelled => $"Yêu cầu tham gia phòng <strong>{request.Room.Name}</strong> của bạn đã được <span style='color:orange;font-weight:bold'>HUỶ</span>.",
                _ => "Trạng thái yêu cầu đã được cập nhật."
            };
            content = content.Replace("{{RoleMessage}}", roleMessage);

            var resultSendMail = DoingMail.SendMail("TeamUp", "Cập nhật trạng thái yêu cầu tham gia phòng", content, request.Requester.Email);
            if (!resultSendMail)
            {
                return new ApiErrorResult<object>("Không thể gửi email tới " + request.Requester.Email);
            }

            return new ApiSuccessResult<object>("Cập nhật trạng thái yêu cầu thành công.");
        }


    }
}
