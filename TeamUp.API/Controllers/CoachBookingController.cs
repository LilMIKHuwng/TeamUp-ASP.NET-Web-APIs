using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Mvc;
using TeamUp.Contract.Services.Interface;
using TeamUp.Core.APIResponse;
using TeamUp.Core.Utils.Enum;
using TeamUp.ModelViews.CoachBookingModelViews;
using TeamUp.Services.Service;

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
            [FromQuery] DateTime? startTime,
            [FromQuery] DateTime? endTime,
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
        public async Task<ActionResult<ApiResult<object>>> Create([FromBody] CreateCoachBookingModelView model)
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
        public async Task<ActionResult<ApiResult<object>>> Update(int id, [FromBody] UpdateCoachBookingModelView model)
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

        /// <summary>
        /// Get total price for a coach in a specific month and year (for owner and admin)
        /// </summary>
        [HttpGet("total-price/coach")]
        public async Task<ActionResult<ApiResult<object>>> GetTotalPriceInMonth(
            [FromQuery] int coachId,
            [FromQuery] string paymentMethod,
            [FromQuery] int month,
            [FromQuery] int year)
        {
            try
            {
                var result = await _coachBookingService.GetTotalPriceInMonthForCoachAndAdmin(coachId, paymentMethod, month, year);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }



        // New API Endpoints
        /// <summary>
        /// Get list of Coach User and Price
        /// </summary>
        [HttpGet("user-coach-totalprice-stats/{coachId}")]
        public async Task<IActionResult> GetCoachBookingStats(int coachId)
        {
            try
            {
                var result = await _coachBookingService.GetCoachBookingStatsAsync(coachId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get list User teach by Coach
        /// </summary>
        [HttpGet("players/{coachId}")]
        public async Task<IActionResult> GetPlayersByCoach(int coachId)
        { 
            try
            {
                var result = await _coachBookingService.GetPlayersByCoachAsync(coachId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get monthly-total for Coach
        /// </summary>
        [HttpGet("monthly-total/{coachId}")]
        public async Task<IActionResult> GetTotalBookingsThisMonthForCoach(int coachId)
        {
            try
            {
                var result = await _coachBookingService.GetTotalBookingsThisMonthByCoachAsync(coachId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get weekly-booked-slots for Coach
        /// </summary>
        [HttpGet("weekly-booked-slots/{coachId}")]
        public async Task<IActionResult> GetBookedSlotsThisWeekForCoach(int coachId)
        {
            try
            {
                var result = await _coachBookingService.GetBookedSlotsThisWeekByCoachAsync(coachId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }


        /// <summary>
        /// Get latest-coach-booking-id
        /// </summary>
        [HttpGet("latest-booking-id")]
        public async Task<IActionResult> GetLatestBookingIdByPlayer([FromQuery] int playerId)
        {
            try
            {
                var result = await _coachBookingService.GetLatestCoachBookingIdByPlayerAsync(playerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
