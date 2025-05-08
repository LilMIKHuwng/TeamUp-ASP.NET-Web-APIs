using TeamUp.Core.APIResponse;
using System.Threading.Tasks;

namespace TeamUp.Contract.Services.Interface
{
    public interface IAIWebsiteService
    {
        Task<ApiResult<string>> GetWebsiteAIResponseAsync(string question);
    }
}
