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

                mapped[i].Requester = _mapper.Map<UserResponseModel>(items[i].Requester);
            }

            return new ApiSuccessResult<BasePaginatedList<RoomJoinRequestModelView>>(
                new BasePaginatedList<RoomJoinRequestModelView>(mapped, totalCount, pageNumber, pageSize));
        }

        public async Task<ApiResult<object>> AddRoomJoinRequestAsync(CreateRoomJoinRequestModelView model)
        {
            var room = await _unitOfWork.GetRepository<Room>().GetByIdAsync(model.RoomId);
            var requester = await _unitOfWork.GetRepository<ApplicationUser>().GetByIdAsync(model.RequesterId);

            if (room == null)
                return new ApiErrorResult<object>("Phòng không tồn tại.");
            if (requester == null)
                return new ApiErrorResult<object>("Người gửi yêu cầu không tồn tại.");

            var newRequest = _mapper.Map<RoomJoinRequest>(model);
            newRequest.Status = RoomJoinRequestStatus.Pending;
            newRequest.RequestedAt = DateTime.Now;

            await _unitOfWork.GetRepository<RoomJoinRequest>().InsertAsync(newRequest);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Yêu cầu tham gia đã được gửi.");
        }

        public async Task<ApiResult<object>> UpdateRoomJoinRequestAsync(int id, UpdateRoomJoinRequestModelView model)
        {
            var repo = _unitOfWork.GetRepository<RoomJoinRequest>();
            var request = await repo.Entities.FirstOrDefaultAsync(r => r.Id == id && !r.DeletedTime.HasValue);

            if (request == null)
                return new ApiErrorResult<object>("Yêu cầu không tồn tại.");

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
                    request.RespondedAt = DateTime.UtcNow;
                }
            }

            request.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");
            request.LastUpdatedTime = DateTimeOffset.UtcNow;

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

            request.DeletedTime = DateTimeOffset.UtcNow;
            request.DeletedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

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

                result[i].Requester = _mapper.Map<UserResponseModel>(requests[i].Requester);
            }

            return new ApiSuccessResult<List<RoomJoinRequestModelView>>(result);
        }
    }
}
