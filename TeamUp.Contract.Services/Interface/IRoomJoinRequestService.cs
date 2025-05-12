using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Core.APIResponse;
using TeamUp.Core;
using TeamUp.ModelViews.CourtModelViews;
using TeamUp.ModelViews.RoomJoinRequestModelViews;

namespace TeamUp.Contract.Services.Interface
{
    public interface IRoomJoinRequestService
    {
        Task<ApiResult<BasePaginatedList<RoomJoinRequestModelView>>> GetAllRoomJoinRequestAsync(int pageNumber, int pageSize, int? requestId, int? roomId, string? status);
        Task<ApiResult<object>> AddRoomJoinRequestAsync(CreateRoomJoinRequestModelView model);
        Task<ApiResult<object>> UpdateRoomJoinRequestAsync(int id, UpdateRoomJoinRequestModelView model);
        Task<ApiResult<object>> DeleteRoomJoinRequestAsync(int id);
        Task<ApiResult<RoomJoinRequestModelView>> GetRoomJoinRequestByIdAsync(int id);
        Task<ApiResult<List<RoomJoinRequestModelView>>> GetAllRoomJoinRequest();
    }
}
