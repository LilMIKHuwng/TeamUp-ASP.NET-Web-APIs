using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Core.APIResponse;
using TeamUp.Core;
using TeamUp.ModelViews.RoleModelViews;
using TeamUp.ModelViews.SportsComplexModelViews;
using TeamUp.Core.Base;
using TeamUp.ModelViews.UserModelViews.Response;

namespace TeamUp.Contract.Services.Interface
{
    public interface ISportsComplexService
    {
        Task<ApiResult<BasePaginatedList<SportsComplexModelView>>> GetAllSportsComplexAsync(int pageNumber, int pageSize, string? name, string? address);
        Task<ApiResult<object>> AddSportsComplexAsync(CreateSportsComplexModelView model);
        Task<ApiResult<object>> UpdateSportsComplexAsync(int id, UpdateSportsComplexModelView model);
        Task<ApiResult<object>> DeleteSportsComplexAsync(int id);
        Task<ApiResult<SportsComplexModelView>> GetSportsComplexByIdAsync(int id);
        Task<ApiResult<List<SportsComplexModelView>>> GetAllSportsComplex();
    }
}
