using Microsoft.AspNetCore.Mvc;
using TeamUp.Contract.Services.Interface;
using TeamUp.Core;
using TeamUp.Core.APIResponse;
using TeamUp.ModelViews.VoucherModelViews;

namespace TeamUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;

        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        /// <summary>
        ///     Get all vouchers with pagination and optional filters
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<BasePaginatedList<VoucherModelView>>> GetAllVouchers(
            [FromQuery] string? code,
            [FromQuery] int? discountPercent,
            int pageNumber = 1,
            int pageSize = 5)
        {
            try
            {
                var result = await _voucherService.GetAllVoucherAsync(pageNumber, pageSize, code, discountPercent);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        ///     Get a voucher by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<VoucherModelView>> GetVoucherById(int id)
        {
            try
            {
                var result = await _voucherService.GetVoucherByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        ///     Create a new voucher
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<ApiResult<object>>> CreateVoucher([FromQuery] CreateVoucherModelView model)
        {
            try
            {
                var result = await _voucherService.AddVoucherAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        ///     Update a voucher
        /// </summary>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<ApiResult<object>>> UpdateVoucher(int id, [FromQuery] UpdateVoucherModelView model)
        {
            try
            {
                var result = await _voucherService.UpdateVoucherAsync(id, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        ///     Delete a voucher
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ApiResult<object>>> DeleteVoucher(int id)
        {
            try
            {
                var result = await _voucherService.DeleteVoucherAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
