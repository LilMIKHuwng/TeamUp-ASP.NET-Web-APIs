using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.Contract.Repositories.Interface;
using TeamUp.Contract.Services.Interface;
using TeamUp.Core.APIResponse;
using TeamUp.Core;
using TeamUp.ModelViews.CourtModelViews;
using TeamUp.ModelViews.RoomModelViews;
using TeamUp.Repositories.Entity;
using Microsoft.EntityFrameworkCore;
using static BabyCare.Core.Utils.SystemConstant;
using TeamUp.ModelViews.SportsComplexModelViews;
using TeamUp.ModelViews.UserModelViews.Response;

namespace TeamUp.Services.Service
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public RoomService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<ApiResult<BasePaginatedList<RoomModelView>>> GetAllRoomAsync(int pageNumber, int pageSize, string? name, int? maxPlayers, string? status)
        {
            var query = _unitOfWork.GetRepository<Room>().Entities
                .Include(r => r.Host)
                .Include(r => r.Court)
                .Where(r => !r.DeletedTime.HasValue);

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(r => r.Name.Contains(name));

            if (maxPlayers.HasValue)
                query = query.Where(r => r.MaxPlayers == maxPlayers.Value);

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(r => r.Status == status);

            int totalCount = await query.CountAsync();

            var paginatedRooms = await query
                .OrderByDescending(r => r.CreatedTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = _mapper.Map<List<RoomModelView>>(paginatedRooms);

            for (int i = 0; i < result.Count; i++)
            {
                result[i].Host  = _mapper.Map<UserResponseModel>(paginatedRooms[i].Host);

                result[i].Court = _mapper.Map<CourtModelView>(paginatedRooms[i].Court);

                result[i].Court.SportsComplexModelView = _mapper.Map<SportsComplexModelView>(paginatedRooms[i].Court.SportsComplex);
            }

            return new ApiSuccessResult<BasePaginatedList<RoomModelView>>(new BasePaginatedList<RoomModelView>(result, totalCount, pageNumber, pageSize));
        }

        public async Task<ApiResult<object>> AddRoomAsync(CreateRoomModelView model)
        {
            var court = await _unitOfWork.GetRepository<Court>().GetByIdAsync(model.CourtId);
            if (court == null || court.DeletedTime.HasValue)
                return new ApiErrorResult<object>("Không tìm thấy sân hợp lệ.");

            var host = await _unitOfWork.GetRepository<ApplicationUser>().GetByIdAsync(model.HostId);
            if (host == null)
                return new ApiErrorResult<object>("Không tìm thấy người tạo phòng.");

            var newRoom = _mapper.Map<Room>(model);
            newRoom.Status = RoomStatus.Waiting;
            newRoom.CreatedBy = model.HostId;
            newRoom.CreatedTime = DateTime.Now;

            await _unitOfWork.GetRepository<Room>().InsertAsync(newRoom);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Tạo phòng thành công.");
        }

        public async Task<ApiResult<object>> UpdateRoomAsync(int id, UpdateRoomModelView model)
        {
            var roomRepo = _unitOfWork.GetRepository<Room>();
            var room = await roomRepo.Entities.FirstOrDefaultAsync(r => r.Id == id && !r.DeletedTime.HasValue);

            if (room == null)
                return new ApiErrorResult<object>("Không tìm thấy phòng.");

            if (model.HostId.HasValue)
                room.HostId = model.HostId.Value;

            if (model.CourtId.HasValue)
            {
                var court = await _unitOfWork.GetRepository<Court>().GetByIdAsync(model.CourtId.Value);
                if (court == null || court.DeletedTime.HasValue)
                    return new ApiErrorResult<object>("Sân không hợp lệ.");
                room.CourtId = model.CourtId.Value;
            }

            if (!string.IsNullOrWhiteSpace(model.Name))
                room.Name = model.Name;

            if (model.MaxPlayers.HasValue)
                room.MaxPlayers = model.MaxPlayers.Value;

            if (!string.IsNullOrWhiteSpace(model.Description))
                room.Description = model.Description;

            if (model.RoomFee.HasValue)
                room.RoomFee = model.RoomFee.Value;

            if (model.ScheduledTime.HasValue)
                room.ScheduledTime = model.ScheduledTime.Value;

            if (!string.IsNullOrWhiteSpace(model.Status))
                room.Status = model.Status;

            room.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");
            room.LastUpdatedTime = DateTime.Now;

            await roomRepo.UpdateAsync(room);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Cập nhật phòng thành công.");
        }

        public async Task<ApiResult<object>> DeleteRoomAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Room>();
            var room = await repo.Entities.FirstOrDefaultAsync(r => r.Id == id && !r.DeletedTime.HasValue);

            if (room == null)
                return new ApiErrorResult<object>("Không tìm thấy phòng.");

            room.DeletedTime = DateTime.Now;
            room.DeletedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            await repo.UpdateAsync(room);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Xóa phòng thành công.");
        }

        public async Task<ApiResult<RoomModelView>> GetRoomByIdAsync(int id)
        {
            var room = await _unitOfWork.GetRepository<Room>().Entities
                .Include(r => r.Court)
                .Include(r => r.Host)
                .FirstOrDefaultAsync(r => r.Id == id && !r.DeletedTime.HasValue);

            if (room == null)
                return new ApiErrorResult<RoomModelView>("Không tìm thấy phòng.");

            var result = _mapper.Map<RoomModelView>(room);

            result.Host = _mapper.Map<UserResponseModel>(room.Host);

            result.Court = _mapper.Map<CourtModelView>(room.Court);

            result.Court.SportsComplexModelView = _mapper.Map<SportsComplexModelView>(room.Court.SportsComplex);

            return new ApiSuccessResult<RoomModelView>(result);
        }

        public async Task<ApiResult<List<RoomModelView>>> GetAllRoom()
        {
            var rooms = await _unitOfWork.GetRepository<Room>().Entities
                .Include(r => r.Court)
                .Include(r => r.Host)
                .OrderByDescending(r => r.CreatedTime)
                .Where(r => !r.DeletedTime.HasValue)
                .ToListAsync();

            var result = _mapper.Map<List<RoomModelView>>(rooms);

            for (int i = 0; i < result.Count; i++)
            {
                result[i].Host = _mapper.Map<UserResponseModel>(rooms[i].Host);

                result[i].Court = _mapper.Map<CourtModelView>(rooms[i].Court);

                result[i].Court.SportsComplexModelView = _mapper.Map<SportsComplexModelView>(rooms[i].Court.SportsComplex);
            }

            return new ApiSuccessResult<List<RoomModelView>>(result);
        }

        public async Task<ApiResult<object>> UpdateRoomStatusAsync(int id, string status)
        {
            var room = await _unitOfWork.GetRepository<Room>().GetByIdAsync(id);
            if (room == null || room.DeletedTime.HasValue)
                return new ApiErrorResult<object>("Không tìm thấy phòng.");

            var validStatuses = new[] {
                    RoomStatus.Waiting,
                    RoomStatus.Full,
                    RoomStatus.InProgress,
                    RoomStatus.Completed,
                    RoomStatus.Cancelled
                };

            if (!validStatuses.Contains(status))
                return new ApiErrorResult<object>("Trạng thái không hợp lệ.");

            room.Status = status;
            room.LastUpdatedTime = DateTime.Now;
            room.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            await _unitOfWork.GetRepository<Room>().UpdateAsync(room);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Cập nhật trạng thái phòng thành công.");
        }
    }

}
