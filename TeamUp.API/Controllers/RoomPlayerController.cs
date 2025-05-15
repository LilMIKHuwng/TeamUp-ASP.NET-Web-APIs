using Microsoft.AspNetCore.Mvc;
using TeamUp.Contract.Services.Interface;
using TeamUp.Core.APIResponse;
using TeamUp.ModelViews.RoomPlayerModelViews;

namespace TeamUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomPlayerController : ControllerBase
    {
        private readonly IRoomPlayerService _roomPlayerService;

        public RoomPlayerController(IRoomPlayerService roomPlayerService)
        {
            _roomPlayerService = roomPlayerService;
        }

        /// <summary>
        /// Get all room players with optional filters and pagination
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<ApiResult<object>>> GetAll(
            [FromQuery] int? roomId,
            [FromQuery] int? playerId,
            [FromQuery] string? status,
            [FromQuery] DateTime? joinAt,
            int pageNumber = 1,
            int pageSize = 10)
        {
            try
            {
                var result = await _roomPlayerService.GetAllRoomPlayerAsync(pageNumber, pageSize, roomId, playerId, joinAt, status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get a room player by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<object>>> GetById(int id)
        {
            try
            {
                var result = await _roomPlayerService.GetRoomPlayerByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Create a new room player (requires accepted join request)
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<ApiResult<object>>> Create([FromBody] CreateRoomPlayerModelView model)
        {
            try
            {
                var result = await _roomPlayerService.AddRoomPlayerAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Update an existing room player (requires accepted join request)
        /// </summary>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<ApiResult<object>>> Update(int id, [FromBody] UpdateRoomPlayerModelView model)
        {
            try
            {
                var result = await _roomPlayerService.UpdateRoomPlayerAsync(id, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Delete a room player by ID
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ApiResult<object>>> Delete(int id)
        {
            try
            {
                var result = await _roomPlayerService.DeleteRoomPlayerAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get all room players without pagination
        /// </summary>
        [HttpGet("all/no-pagination")]
        public async Task<ActionResult<ApiResult<List<RoomPlayerModelView>>>> GetAllNoPagination()
        {
            try
            {
                var result = await _roomPlayerService.GetAllRoomPlayer();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Update the status of a room player
        /// </summary>
        [HttpPatch("status/{id}")]
        public async Task<ActionResult<ApiResult<object>>> UpdateStatus(int id, [FromQuery] string status)
        {
            try
            {
                var result = await _roomPlayerService.UpdateRoomPlayergStatusAsync(id, status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
