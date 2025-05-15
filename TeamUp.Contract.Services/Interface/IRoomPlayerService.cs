using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Core.APIResponse;
using TeamUp.Core;
using TeamUp.ModelViews.CoachBookingModelViews;
using TeamUp.ModelViews.RoomPlayerModelViews;

namespace TeamUp.Contract.Services.Interface
{
    public interface IRoomPlayerService
    {
        Task<ApiResult<BasePaginatedList<RoomPlayerModelView>>> GetAllRoomPlayerAsync(int pageNumber, int pageSize, int? roomId, int? playerId, DateTime? joinAt, string? status);
        Task<ApiResult<object>> AddRoomPlayerAsync(CreateRoomPlayerModelView model);
        Task<ApiResult<object>> UpdateRoomPlayerAsync(int id, UpdateRoomPlayerModelView model);
        Task<ApiResult<object>> DeleteRoomPlayerAsync(int id);
        Task<ApiResult<RoomPlayerModelView>> GetRoomPlayerByIdAsync(int id);
        Task<ApiResult<List<RoomPlayerModelView>>> GetAllRoomPlayer();
        Task<ApiResult<object>> UpdateRoomPlayergStatusAsync(int id, string status);
    }
}
