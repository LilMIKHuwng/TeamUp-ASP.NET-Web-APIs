using Microsoft.AspNetCore.Mvc;
using TeamUp.Contract.Services.Interface;
using TeamUp.Core.APIResponse;
using TeamUp.ModelViews.PackageModelViews;

namespace TeamUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _packageService;

        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        /// <summary>
        /// Get all packages with pagination and optional filters
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<ApiResult<object>>> GetAll(
            [FromQuery] string? name,
            [FromQuery] string? type,
            [FromQuery] decimal? price,
            [FromQuery] int? durationDays,
            int pageNumber = 1,
            int pageSize = 5)
        {
            try
            {
                var result = await _packageService.GetAllPackageAsync(pageNumber, pageSize, name, type, price, durationDays);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get a package by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<object>>> GetById(int id)
        {
            try
            {
                var result = await _packageService.GetPackageByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Create a new package
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<ApiResult<object>>> Create([FromBody] CreatePackageModelView model)
        {
            try
            {
                var result = await _packageService.AddPackageAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Update an existing package
        /// </summary>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<ApiResult<object>>> Update(int id, [FromBody] UpdatePackageModelView model)
        {
            try
            {
                var result = await _packageService.UpdatePackageAsync(id, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Delete a package by ID
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ApiResult<object>>> Delete(int id)
        {
            try
            {
                var result = await _packageService.DeletePackageAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get all packages (no pagination)
        /// </summary>
        [HttpGet("all/no-pagination")]
        public async Task<ActionResult<ApiResult<List<PackageModelView>>>> GetAllNoPagination()
        {
            try
            {
                var result = await _packageService.GetAllPackage();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
