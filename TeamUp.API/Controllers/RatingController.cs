using Microsoft.AspNetCore.Mvc;
using TeamUp.Contract.Services.Interface;
using TeamUp.Core.APIResponse;
using TeamUp.ModelViews.RatingModelViews;

namespace TeamUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        /// <summary>
        /// Get all ratings with optional filters and pagination
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<ApiResult<object>>> GetAll(
            [FromQuery] int? reviewerId,
            [FromQuery] int? revieweeId,
            [FromQuery] int? ratingValue,
            [FromQuery] string? comment,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            try
            {
                var result = await _ratingService.GetAllRatingAsync(pageNumber, pageSize, reviewerId, revieweeId, ratingValue, comment);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get all ratings (no pagination)
        /// </summary>
        [HttpGet("all/no-pagination")]
        public async Task<ActionResult<ApiResult<List<RatingModelView>>>> GetAllNoPagination()
        {
            try
            {
                var result = await _ratingService.GetAllRating();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<List<RatingModelView>>(ex.Message));
            }
        }

        /// <summary>
        /// Get rating by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<RatingModelView>>> GetById(int id)
        {
            try
            {
                var result = await _ratingService.GetRatingByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<RatingModelView>(ex.Message));
            }
        }

        /// <summary>
        /// Create a new rating
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<ApiResult<object>>> Create([FromForm] CreateRatingModelView model)
        {
            try
            {
                var result = await _ratingService.AddRatingAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Update a rating
        /// </summary>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<ApiResult<object>>> Update(int id, [FromForm] UpdateRatingModelView model)
        {
            try
            {
                var result = await _ratingService.UpdateRatingAsync(id, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Delete a rating
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ApiResult<object>>> Delete(int id)
        {
            try
            {
                var result = await _ratingService.DeleteRatingAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get average and count rating for a specific user
        /// </summary>
        [HttpGet("average-count/{userId}")]
        public async Task<ActionResult<ApiResult<double>>> GetAverageRating(int userId)
        {
            try
            {
                var result = await _ratingService.GetRatingSummaryForUserAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<double>(ex.Message));
            }
        }

        /// <summary>
        /// Get rating statistics (count of each star value) for a user
        /// </summary>
        [HttpGet("stats/{userId}")]
        public async Task<ActionResult<ApiResult<Dictionary<int, int>>>> GetRatingStats(int userId)
        {
            try
            {
                var result = await _ratingService.GetRatingStatsForUserAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<Dictionary<int, int>>(ex.Message));
            }
        }

        /// <summary>
        /// Check if a reviewer has already rated a reviewee
        /// </summary>
        [HttpGet("check-rated")]
        public async Task<ActionResult<ApiResult<bool>>> CheckIfRated(
            [FromQuery] int reviewerId,
            [FromQuery] int revieweeId)
        {
            try
            {
                var result = await _ratingService.CheckIfUserAlreadyRatedAsync(reviewerId, revieweeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<bool>(ex.Message));
            }
        }

        /// <summary>
        /// Get latest ratings for a user
        /// </summary>
        [HttpGet("latest")]
        public async Task<ActionResult<ApiResult<List<RatingModelView>>>> GetLatestRatings(
            [FromQuery] int revieweeId,
            [FromQuery] int take = 5)
        {
            try
            {
                var result = await _ratingService.GetLatestRatingsForUserAsync(revieweeId, take);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<List<RatingModelView>>(ex.Message));
            }
        }
    }
}
