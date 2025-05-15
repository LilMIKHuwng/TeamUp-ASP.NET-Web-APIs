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
using TeamUp.ModelViews.RoomPlayerModelViews;
using TeamUp.Repositories.Entity;
using Microsoft.EntityFrameworkCore;
using Azure.Core;
using TeamUp.ModelViews.CourtModelViews;
using TeamUp.ModelViews.RoomModelViews;
using TeamUp.ModelViews.SportsComplexModelViews;
using TeamUp.ModelViews.UserModelViews.Response;

namespace TeamUp.Services.Service
{
    public class RoomPlayerService : IRoomPlayerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public RoomPlayerService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<ApiResult<object>> AddRoomPlayerAsync(CreateRoomPlayerModelView model)
        {
            var room = await _unitOfWork.GetRepository<Room>().GetByIdAsync(model.RoomId);
            var player = await _unitOfWork.GetRepository<ApplicationUser>().GetByIdAsync(model.PlayerId);

            if (room == null || room.DeletedTime.HasValue)
                return new ApiErrorResult<object>("Không tìm thấy phòng.");

            if (player == null)
                return new ApiErrorResult<object>("Không tìm thấy người chơi.");

            var joinRequest = await _unitOfWork.GetRepository<RoomJoinRequest>().Entities
                .Where(r => r.RoomId == model.RoomId && r.RequesterId == model.PlayerId)
                .OrderByDescending(r => r.RequestedAt)
                .FirstOrDefaultAsync();

            if (joinRequest == null || joinRequest.Status != RoomJoinRequestStatus.Accepted)
                return new ApiErrorResult<object>("Người chơi chưa được chấp nhận vào phòng.");

            var newRoomPlayer = _mapper.Map<RoomPlayer>(model);
            newRoomPlayer.Status = RoomPlayerStatus.Accepted;
            newRoomPlayer.JoinedAt = DateTime.Now;

            newRoomPlayer.CreatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");
            newRoomPlayer.CreatedTime = DateTime.Now;

            await _unitOfWork.GetRepository<RoomPlayer>().InsertAsync(newRoomPlayer);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Thêm người chơi vào phòng thành công.");
        }

        public async Task<ApiResult<object>> DeleteRoomPlayerAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<RoomPlayer>();
            var entity = await repo.GetByIdAsync(id);
            if (entity == null)
                return new ApiErrorResult<object>("Không tìm thấy dữ liệu.");

            entity.DeletedTime = DateTimeOffset.UtcNow;
            entity.DeletedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");
            entity.Status = RoomPlayerStatus.Cancelled;

            await repo.UpdateAsync(entity);
            await _unitOfWork.SaveAsync();
            return new ApiSuccessResult<object>("Xóa thành công.");
        }

        public async Task<ApiResult<BasePaginatedList<RoomPlayerModelView>>> GetAllRoomPlayerAsync(int pageNumber, int pageSize, int? roomId, int? playerId, DateTime? joinAt, string? status)
        {
            var query = _unitOfWork.GetRepository<RoomPlayer>().Entities
                .Include(rp => rp.Room)
                .Include(rp => rp.Player)
                .Where(rp => rp.Room.DeletedTime == null);

            if (roomId.HasValue)
                query = query.Where(rp => rp.RoomId == roomId.Value);

            if (playerId.HasValue)
                query = query.Where(rp => rp.PlayerId == playerId.Value);

            if (joinAt.HasValue)
                query = query.Where(rp => rp.JoinedAt.Date == joinAt.Value.Date);

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(rp => rp.Status == status);

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(rp => rp.JoinedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = _mapper.Map<List<RoomPlayerModelView>>(items);

            for (int i = 0; i < result.Count; i++)
            {
                result[i].Room = _mapper.Map<RoomModelView>(items[i].Room);

                result[i].Room.Court = _mapper.Map<CourtModelView>(items[i].Room.Court);

                result[i].Room.Court.SportsComplexModelView = _mapper.Map<SportsComplexModelView>(items[i].Room.Court.SportsComplex);

                result[i].Player = _mapper.Map<UserResponseModel>(items[i].Player);
            }

            return new ApiSuccessResult<BasePaginatedList<RoomPlayerModelView>>(
                new BasePaginatedList<RoomPlayerModelView>(result, totalCount, pageNumber, pageSize));
        }

        public async Task<ApiResult<List<RoomPlayerModelView>>> GetAllRoomPlayer()
        {
            var entities = await _unitOfWork.GetRepository<RoomPlayer>().Entities
                .Include(rp => rp.Room)
                .Include(rp => rp.Player)
                .Where(rp => rp.Room.DeletedTime == null)
                .OrderByDescending(rp => rp.JoinedAt)
                .ToListAsync();

            var result = _mapper.Map<List<RoomPlayerModelView>>(entities);

            for (int i = 0; i < result.Count; i++)
            {
                result[i].Room = _mapper.Map<RoomModelView>(entities[i].Room);

                result[i].Room.Court = _mapper.Map<CourtModelView>(entities[i].Room.Court);

                result[i].Room.Court.SportsComplexModelView = _mapper.Map<SportsComplexModelView>(entities[i].Room.Court.SportsComplex);

                result[i].Player = _mapper.Map<UserResponseModel>(entities[i].Player);
            }

            return new ApiSuccessResult<List<RoomPlayerModelView>>(result);
        }

        public async Task<ApiResult<RoomPlayerModelView>> GetRoomPlayerByIdAsync(int id)
        {
            var entity = await _unitOfWork.GetRepository<RoomPlayer>().Entities
                .Include(rp => rp.Room)
                .Include(rp => rp.Player)
                .FirstOrDefaultAsync(rp => rp.Id == id);

            if (entity == null)
                return new ApiErrorResult<RoomPlayerModelView>("Không tìm thấy thông tin.");

            var result = _mapper.Map<RoomPlayerModelView>(entity);

            result.Room = _mapper.Map<RoomModelView>(entity.Room);

            result.Room.Court = _mapper.Map<CourtModelView>(entity.Room.Court);

            result.Room.Court.SportsComplexModelView = _mapper.Map<SportsComplexModelView>(entity.Room.Court.SportsComplex);

            result.Player = _mapper.Map<UserResponseModel>(entity.Player);

            return new ApiSuccessResult<RoomPlayerModelView>(result);
        }

        public async Task<ApiResult<object>> UpdateRoomPlayerAsync(int id, UpdateRoomPlayerModelView model)
        {
            var repo = _unitOfWork.GetRepository<RoomPlayer>();
            var entity = await repo.GetByIdAsync(id);

            if (entity == null)
                return new ApiErrorResult<object>("Không tìm thấy người chơi trong phòng.");

            int roomId = model.RoomId ?? entity.RoomId;
            int playerId = model.PlayerId ?? entity.PlayerId;

            var joinRequest = await _unitOfWork.GetRepository<RoomJoinRequest>().Entities
                .Where(r => r.RoomId == roomId && r.RequesterId == playerId)
                .OrderByDescending(r => r.RequestedAt)
                .FirstOrDefaultAsync();

            if (joinRequest == null || joinRequest.Status != RoomJoinRequestStatus.Accepted)
                return new ApiErrorResult<object>("Người chơi chưa được chấp nhận vào phòng.");

            if (model.PlayerId.HasValue)
                entity.PlayerId = model.PlayerId.Value;

            if (model.RoomId.HasValue)
                entity.RoomId = model.RoomId.Value;

            if (!string.IsNullOrWhiteSpace(model.Status))
            {
                var validStatuses = new[]
                {
                    RoomPlayerStatus.Accepted,
                    RoomPlayerStatus.InProgress,
                    RoomPlayerStatus.Completed,
                    RoomPlayerStatus.Cancelled
                };

                if (!validStatuses.Contains(model.Status))
                    return new ApiErrorResult<object>("Trạng thái không hợp lệ.");

                entity.Status = model.Status;
            }

            entity.LastUpdatedTime = DateTime.Now;
            entity.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            await repo.UpdateAsync(entity);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Cập nhật thông tin thành công.");
        }

        public async Task<ApiResult<object>> UpdateRoomPlayergStatusAsync(int id, string status)
        {
            var entity = await _unitOfWork.GetRepository<RoomPlayer>().GetByIdAsync(id);

            if (entity == null)
                return new ApiErrorResult<object>("Không tìm thấy người chơi trong phòng.");

            var validStatuses = new[]
            {
                RoomPlayerStatus.Accepted,
                RoomPlayerStatus.InProgress,
                RoomPlayerStatus.Completed,
                RoomPlayerStatus.Cancelled
            };

            if (!validStatuses.Contains(status))
                return new ApiErrorResult<object>("Trạng thái không hợp lệ.");

            entity.Status = status;
            entity.LastUpdatedTime = DateTime.Now;
            entity.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            await _unitOfWork.GetRepository<RoomPlayer>().UpdateAsync(entity);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Cập nhật trạng thái thành công.");
        }
    }
}
