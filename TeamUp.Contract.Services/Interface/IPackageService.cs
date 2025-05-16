using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Core.APIResponse;
using TeamUp.Core;
using TeamUp.ModelViews.CourtModelViews;
using TeamUp.ModelViews.PackageModelViews;

namespace TeamUp.Contract.Services.Interface
{
    public interface IPackageService
    {
        Task<ApiResult<BasePaginatedList<PackageModelView>>> GetAllPackageAsync(int pageNumber, int pageSize, string? name, string? type, decimal? price, int? durationDays);
        Task<ApiResult<object>> AddPackageAsync(CreatePackageModelView model);
        Task<ApiResult<object>> UpdatePackageAsync(int id, UpdatePackageModelView model);
        Task<ApiResult<object>> DeletePackageAsync(int id);
        Task<ApiResult<PackageModelView>> GetPackageByIdAsync(int id);
        Task<ApiResult<List<PackageModelView>>> GetAllPackage();
    }
}
