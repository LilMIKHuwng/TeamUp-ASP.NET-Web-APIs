using Microsoft.AspNetCore.Mvc;
using TeamUp.Contract.Services.Interface;
using TeamUp.Core.APIResponse;
using TeamUp.ModelViews.CourtBookingModelViews;
using TeamUp.Services.Service;

namespace TeamUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourtBookingController : ControllerBase
    {
        private readonly ICourtBookingService _courtBookingService;

        public CourtBookingController(ICourtBookingService courtBookingService)
        {
            _courtBookingService = courtBookingService;
        }

        /// <summary>
        /// Get all court bookings with optional filters and pagination
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<ApiResult<object>>> GetAll(
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
                var result = await _courtBookingService.GetAllCourtBookingAsync(pageNumber, pageSize, userId, courtId, startTime, endTime, status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get a booking by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<object>>> GetById(int id)
        {
            try
            {
                var result = await _courtBookingService.GetCourtBookingByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Create a new court booking
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<ApiResult<object>>> Create([FromForm] CreateCourtBookingModelView model)
        {
            try
            {
                var result = await _courtBookingService.AddCourtBookingAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Update a court booking
        /// </summary>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<ApiResult<object>>> Update(int id, [FromForm] UpdateCourtBookingModelView model)
        {
            try
            {
                var result = await _courtBookingService.UpdateCourtBookingAsync(id, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Update the status or payment status of a booking
        /// </summary>
        [HttpPatch("status/{id}")]
        public async Task<ActionResult<ApiResult<object>>> UpdateStatus(int id, [FromQuery] string? status, [FromQuery] string? paymentStatus)
        {
            try
            {
                var result = await _courtBookingService.UpdateCourtBookingStatusAsync(id, status, paymentStatus);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Delete a booking
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ApiResult<object>>> Delete(int id)
        {
            try
            {
                var result = await _courtBookingService.DeleteCourtBookingAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get all court bookings (no pagination)
        /// </summary>
        [HttpGet("all/no-pagination")]
        public async Task<ActionResult<ApiResult<List<CourtBookingModelView>>>> GetAllNoPagination()
        {
            try
            {
                var result = await _courtBookingService.GetAllCourtBooking();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get total price for a court in a specific month and year (for owner and admin)
        /// </summary>
        [HttpGet("total-price/owner")]
        public async Task<ActionResult<ApiResult<object>>> GetTotalPriceInMonth(
            [FromQuery] int courtId,
            [FromQuery] string paymentMethod,
            [FromQuery] int month,
            [FromQuery] int year)
        {
            try
            {
                var result = await _courtBookingService.GetTotalPriceInMonthForOwnerAndAdmin(courtId, paymentMethod, month, year);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get list of available (free) hours for a specific court
        /// </summary>
        [HttpGet("free-hours")]
        public async Task<ActionResult<ApiResult<List<object>>>> GetFreeHours([FromQuery] int courtId, [FromQuery] DateTime startDate)
        {
            try
            {
                var result = await _courtBookingService.GetHourFreeInCourt(courtId, startDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get list of Court User and Price
        /// </summary>
        [HttpGet("user-court-totalprice-stats/owner")]
        public async Task<ActionResult<ApiResult<object>>> GetStatsByOwner([FromQuery] int ownerId)
        {
            try
            {
                var result = await _courtBookingService.GetCourtBookingStatsByOwnerAsync(ownerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get most-booked-by-owner
        /// </summary>
        [HttpGet("stats/most-booked-by-owner")]
        public async Task<ActionResult<ApiResult<object>>> GetMostBookedCourtByOwner([FromQuery] int ownerId)
        {
            try
            {
                var result = await _courtBookingService.GetMostBookedCourtByOwnerAsync(ownerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }


        /// <summary>
        /// Get weekly-booked-slots for Court
        /// </summary>
        [HttpGet("weekly-booked-slots/{courtId}")]
        public async Task<IActionResult> GetBookedSlotsThisWeekForCourt(int courtId)
        {
            try
            {
                var result = await _courtBookingService.GetBookedSlotsThisWeekByCourtAsync(courtId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
