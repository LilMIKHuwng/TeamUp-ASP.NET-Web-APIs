using Microsoft.AspNetCore.Mvc;
using TeamUp.Contract.Services.Interface;
using TeamUp.Core.APIResponse;
using TeamUp.ModelViews.SportsComplexModelViews;

namespace TeamUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportsComplexController : ControllerBase
    {
        private readonly ISportsComplexService _sportsComplexService;

        public SportsComplexController(ISportsComplexService sportsComplexService)
        {
            _sportsComplexService = sportsComplexService;
        }

        /// <summary>
        /// Get all sports complexes with pagination and optional filters
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<ApiResult<object>>> GetAll(
            [FromQuery] string? name,
            [FromQuery] string? address,
            int pageNumber = 1,
            int pageSize = 5)
        {
            try
            {
                var result = await _sportsComplexService.GetAllSportsComplexAsync(pageNumber, pageSize, name, address);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get a sports complex by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<SportsComplexModelView>>> GetById(int id)
        {
            try
            {
                var result = await _sportsComplexService.GetSportsComplexByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Create a new sports complex
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<ApiResult<object>>> Create([FromForm] CreateSportsComplexModelView model)
        {
            try
            {
                var result = await _sportsComplexService.AddSportsComplexAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Update an existing sports complex
        /// </summary>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<ApiResult<object>>> Update(int id, [FromForm] UpdateSportsComplexModelView model)
        {
            try
            {
                var result = await _sportsComplexService.UpdateSportsComplexAsync(id, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Delete a sports complex by ID
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ApiResult<object>>> Delete(int id)
        {
            try
            {
                var result = await _sportsComplexService.DeleteSportsComplexAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get all sports complexes (no pagination)
        /// </summary>
        [HttpGet("all/no-pagination")]
        public async Task<ActionResult<ApiResult<List<SportsComplexModelView>>>> GetAllNoPagination()
        {
            try
            {
                var result = await _sportsComplexService.GetAllSportsComplex();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
