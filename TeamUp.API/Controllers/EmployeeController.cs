using Microsoft.AspNetCore.Mvc;
using TeamUp.Contract.Services.Interface;
using TeamUp.Core.APIResponse;
using TeamUp.Core.Base;
using TeamUp.ModelViews.UserModelViews.Request;

namespace TeamUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IUserService _userService;

        public EmployeeController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateEmployee([FromForm] CreateEmployeeRequest request)
        {
            try
            {
                var result = await _userService.CreateEmployee(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpPut("update-coach-profile")]
        public async Task<IActionResult> UpdateCoachProfile([FromForm] UpdateEmployeeProfileRequest request)
        {
            try
            {
                var result = await _userService.UpdateCoachProfile(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateEmployeeStatus([FromBody] UpdateUserStatusRequest request)
        {
            try
            {
                var result = await _userService.UpdateEmployeeStatus(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteEmployee([FromBody] DeleteUserRequest request)
        {
            try
            {
                var result = await _userService.DeleteEmployee(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpPost("get-owners-pagination")]
        public async Task<IActionResult> GetOwnerPagination([FromBody] BaseSearchRequest request)
        {
            try
            {
                var result = await _userService.GetOwnerPagination(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpGet("get-all-owners")]
        public async Task<IActionResult> GetAllOwner()
        {
            try
            {
                var result = await _userService.GetAllOwner();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpPost("get-coaches-pagination")]
        public async Task<IActionResult> GetCoachPagination([FromBody] BaseSearchRequest request)
        {
            try
            {
                var result = await _userService.GetCoachPagination(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpGet("get-all-coaches")]
        public async Task<IActionResult> GetAllCoach()
        {
            try
            {
                var result = await _userService.GetAllCoach();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllEmployee()
        {
            try
            {
                var result = await _userService.GetAllEmployee();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var result = await _userService.GetEmployeeById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpGet("statuses")]
        public IActionResult GetEmployeeStatuses()
        {
            try
            {
                var result = _userService.GetEmployeeStatus();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
