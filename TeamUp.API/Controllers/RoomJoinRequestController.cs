using Microsoft.AspNetCore.Mvc;
using TeamUp.Contract.Services.Interface;
using TeamUp.Core.APIResponse;
using TeamUp.ModelViews.RoomJoinRequestModelViews;

namespace TeamUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomJoinRequestController : ControllerBase
    {
        private readonly IRoomJoinRequestService _roomJoinRequestService;

        public RoomJoinRequestController(IRoomJoinRequestService roomJoinRequestService)
        {
            _roomJoinRequestService = roomJoinRequestService;
        }

        /// <summary>
        /// Get all room join requests with pagination and optional filters
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<ApiResult<object>>> GetAll(
            [FromQuery] int? roomId,
            [FromQuery] int? requesterId,
            [FromQuery] string? status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            try
            {
                var result = await _roomJoinRequestService.GetAllRoomJoinRequestAsync(pageNumber, pageSize, requesterId, roomId, status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get a room join request by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<object>>> GetById(int id)
        {
            try
            {
                var result = await _roomJoinRequestService.GetRoomJoinRequestByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Create a new room join request
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<ApiResult<object>>> Create([FromBody] CreateRoomJoinRequestModelView model)
        {
            try
            {
                var result = await _roomJoinRequestService.AddRoomJoinRequestAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Update an existing room join request
        /// </summary>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<ApiResult<object>>> Update(int id, [FromBody] UpdateRoomJoinRequestModelView model)
        {
            try
            {
                var result = await _roomJoinRequestService.UpdateRoomJoinRequestAsync(id, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Delete a room join request by ID
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ApiResult<object>>> Delete(int id)
        {
            try
            {
                var result = await _roomJoinRequestService.DeleteRoomJoinRequestAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get all room join requests (no pagination)
        /// </summary>
        [HttpGet("all/no-pagination")]
        public async Task<ActionResult<ApiResult<List<RoomJoinRequestModelView>>>> GetAllNoPagination()
        {
            try
            {
                var result = await _roomJoinRequestService.GetAllRoomJoinRequest();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Update the status of a room join request
        /// </summary>
        [HttpPatch("status/{id}")]
        public async Task<ActionResult<ApiResult<object>>> UpdateStatus(int id, [FromQuery] string status)
        {
            try
            {
                var result = await _roomJoinRequestService.UpdateRoomJoinRequestStatusAsync(id, status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
