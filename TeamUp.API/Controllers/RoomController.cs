using Microsoft.AspNetCore.Mvc;
using TeamUp.Contract.Services.Interface;
using TeamUp.Core.APIResponse;
using TeamUp.ModelViews.RoomModelViews;

namespace TeamUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        /// <summary>
        /// Get all rooms with pagination and optional filters
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<ApiResult<object>>> GetAll(
            [FromQuery] string? name,
            [FromQuery] int? maxPlayers,
            [FromQuery] string? status,
            [FromQuery] int? hostId,
            [FromQuery] decimal? maxRoomFee,
            [FromQuery] DateTime? date,
            [FromQuery] TimeSpan? startTime,
            [FromQuery] TimeSpan? endTime,
            [FromQuery] string? type,
            int pageNumber = 1,
            int pageSize = 5)
        {
            try
            {
                var result = await _roomService.GetAllRoomAsync(
                    pageNumber,
                    pageSize,
                    name,
                    maxPlayers,
                    status,
                    hostId,
                    maxRoomFee,
                    date,
                    startTime,
                    endTime,
                    type);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }


        /// <summary>
        /// Get a room by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<object>>> GetById(int id)
        {
            try
            {
                var result = await _roomService.GetRoomByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Create a new room
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<ApiResult<object>>> Create([FromBody] CreateRoomModelView model)
        {
            try
            {
                var result = await _roomService.AddRoomAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Update an existing room
        /// </summary>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<ApiResult<object>>> Update(int id, [FromBody] UpdateRoomModelView model)
        {
            try
            {
                var result = await _roomService.UpdateRoomAsync(id, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Delete a room by ID
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ApiResult<object>>> Delete(int id)
        {
            try
            {
                var result = await _roomService.DeleteRoomAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get all rooms (no pagination)
        /// </summary>
        [HttpGet("all/no-pagination")]
        public async Task<ActionResult<ApiResult<List<RoomModelView>>>> GetAllNoPagination()
        {
            try
            {
                var result = await _roomService.GetAllRoom(); 
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Update the status of a room
        /// </summary>
        [HttpPatch("status/{id}")]
        public async Task<ActionResult<ApiResult<object>>> UpdateStatus(int id, [FromQuery] string status)
        {
            try
            {
                var result = await _roomService.UpdateRoomStatusAsync(id, status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
