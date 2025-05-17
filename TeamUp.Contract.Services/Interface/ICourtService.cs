using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Core.APIResponse;
using TeamUp.Core;
using TeamUp.ModelViews.CourtModelViews;

namespace TeamUp.Contract.Services.Interface
{
    public interface ICourtService
    {
        Task<ApiResult<BasePaginatedList<CourtModelView>>> GetAllCourtxAsync(int pageNumber, int pageSize, string? name, decimal? pricePerHour, int? sportId);
        Task<ApiResult<object>> AddCourtAsync(CreateCourtModelView model);
        Task<ApiResult<object>> UpdateCourtAsync(int id, UpdateCourtModelView model);
        Task<ApiResult<object>> DeleteCourtAsync(int id);
        Task<ApiResult<CourtModelView>> GetCourtByIdAsync(int id);
        Task<ApiResult<List<CourtModelView>>> GetAllCourt();
    }
}
