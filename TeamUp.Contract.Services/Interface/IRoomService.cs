using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Core.APIResponse;
using TeamUp.Core;
using TeamUp.ModelViews.CourtModelViews;
using TeamUp.ModelViews.RoomModelViews;

namespace TeamUp.Contract.Services.Interface
{
    public interface IRoomService
    {
        Task<ApiResult<BasePaginatedList<RoomModelView>>> GetAllRoomAsync(
            int pageNumber,
            int pageSize,
            string? name,
            int? maxPlayers,
            string? status,
            int? hostId,
            decimal? maxRoomFee,
            DateTime? date,
            TimeSpan? startTime,
            TimeSpan? endTime,
            string? type);
        Task<ApiResult<object>> AddRoomAsync(CreateRoomModelView model);
        Task<ApiResult<object>> UpdateRoomAsync(int id, UpdateRoomModelView model);
        Task<ApiResult<object>> DeleteRoomAsync(int id);
        Task<ApiResult<RoomModelView>> GetRoomByIdAsync(int id);
        Task<ApiResult<List<RoomModelView>>> GetAllRoom();
        Task<ApiResult<object>> UpdateRoomStatusAsync(int id, string status);
    }
}
