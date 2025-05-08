using TeamUp.Contract.Repositories.Entity;
using TeamUp.Contract.Services.Interface;
using TeamUp.Services.Service;
using Firebase.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TeamUp.API.Controllers
{
    [Route("api/ai-web")]
    [ApiController]
    public class AIWebsiteController : ControllerBase
    {
        private readonly IAIWebsiteService _aiWebsiteService;

        public AIWebsiteController(IAIWebsiteService aiWebsiteService)
        {
            _aiWebsiteService = aiWebsiteService;
        }
        /// <summary>
        /// Nhận câu hỏi từ người dùng và trả lời bằng AI dựa trên dữ liệu của Website
        /// </summary>
        [HttpGet("get-website-response")]
        public async Task<IActionResult> GetWebsiteResponse(string question)
        {
            try
            {
                var result = await _aiWebsiteService.GetWebsiteAIResponseAsync(question);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new TeamUp.Core.APIResponse.ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
