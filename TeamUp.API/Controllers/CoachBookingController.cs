using Microsoft.AspNetCore.Mvc;
using TeamUp.Contract.Services.Interface;
using TeamUp.Core.APIResponse;
using TeamUp.ModelViews.CoachBookingModelViews;

namespace TeamUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachBookingController : ControllerBase
    {
        private readonly ICoachBookingService _coachBookingService;

        public CoachBookingController(ICoachBookingService coachBookingService)
        {
            _coachBookingService = coachBookingService;
        }

        /// <summary>
        /// Get all coach bookings with optional filters and pagination
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<ApiResult<object>>> GetAll(
            [FromQuery] int? coachId,
            [FromQuery] int? userId,
            [FromQuery] int? courtId,
            [FromQuery] TimeSpan? startTime,
            [FromQuery] TimeSpan? endTime,
            [FromQuery] string? status,
            int pageNumber = 1,
            int pageSize = 5)
        {
            try
            {
                var result = await _coachBookingService.GetAllCoachBookingAsync(pageNumber, pageSize, coachId, userId, courtId, startTime, endTime, status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get a coach booking by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<object>>> GetById(int id)
        {
            try
            {
                var result = await _coachBookingService.GetCoachBookingByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Create a new coach booking
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<ApiResult<object>>> Create([FromForm] CreateCoachBookingModelView model)
        {
            try
            {
                var result = await _coachBookingService.AddCoachBookingAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Update a coach booking
        /// </summary>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<ApiResult<object>>> Update(int id, [FromForm] UpdateCoachBookingModelView model)
        {
            try
            {
                var result = await _coachBookingService.UpdateCoachBookingAsync(id, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Update the status or payment status of a coach booking
        /// </summary>
        [HttpPatch("status/{id}")]
        public async Task<ActionResult<ApiResult<object>>> UpdateStatus(int id, [FromQuery] string? status, [FromQuery] string? paymentStatus)
        {
            try
            {
                var result = await _coachBookingService.UpdateCoachBookingStatusAsync(id, status, paymentStatus);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Delete a coach booking
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ApiResult<object>>> Delete(int id)
        {
            try
            {
                var result = await _coachBookingService.DeleteCoachBookingAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get all coach bookings (no pagination)
        /// </summary>
        [HttpGet("all/no-pagination")]
        public async Task<ActionResult<ApiResult<List<CoachBookingModelView>>>> GetAllNoPagination()
        {
            try
            {
                var result = await _coachBookingService.GetAllCoachBooking();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
