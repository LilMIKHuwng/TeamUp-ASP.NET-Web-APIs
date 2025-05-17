using Microsoft.AspNetCore.Mvc;
using TeamUp.Contract.Services.Interface;
using TeamUp.Core.APIResponse;
using TeamUp.ModelViews.CourtModelViews;

namespace TeamUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourtController : ControllerBase
    {
        private readonly ICourtService _courtService;

        public CourtController(ICourtService courtService)
        {
            _courtService = courtService;
        }

        /// <summary>
        /// Get all courts with pagination and optional filters
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<ApiResult<object>>> GetAll(
            [FromQuery] string? name,
            [FromQuery] decimal? pricePerHour,
            [FromQuery] string? address,
            [FromQuery] int? sportId,
            [FromQuery] string? type,
            int pageNumber = 1,
            int pageSize = 5)
        {
            try
            {
                var result = await _courtService.GetAllCourtxAsync(pageNumber, pageSize, name, pricePerHour, address, sportId, type);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get a court by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<object>>> GetById(int id)
        {
            try
            {
                var result = await _courtService.GetCourtByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Create a new court
        /// </summary>
        [HttpPost("create")]
        [RequestSizeLimit(10_000_000)] // tùy chỉnh giới hạn nếu upload ảnh
        public async Task<ActionResult<ApiResult<object>>> Create([FromForm] CreateCourtModelView model)
        {
            try
            {
                var result = await _courtService.AddCourtAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Update an existing court
        /// </summary>
        [HttpPut("update/{id}")]
        [RequestSizeLimit(10_000_000)]
        public async Task<ActionResult<ApiResult<object>>> Update(int id, [FromForm] UpdateCourtModelView model)
        {
            try
            {
                var result = await _courtService.UpdateCourtAsync(id, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Delete a court by ID
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ApiResult<object>>> Delete(int id)
        {
            try
            {
                var result = await _courtService.DeleteCourtAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get all courts (no pagination)
        /// </summary>
        [HttpGet("all/no-pagination")]
        public async Task<ActionResult<ApiResult<List<CourtModelView>>>> GetAllNoPagination()
        {
            try
            {
                var result = await _courtService.GetAllCourt();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
