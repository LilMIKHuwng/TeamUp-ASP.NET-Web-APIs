using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Core.APIResponse;
using TeamUp.Core;
using TeamUp.ModelViews.RoleModelViews;
using TeamUp.ModelViews.VoucherModelViews;

namespace TeamUp.Contract.Services.Interface
{
    public interface IVoucherService
    {
        Task<ApiResult<BasePaginatedList<VoucherModelView>>> GetAllVoucherAsync(int pageNumber, int pageSize, string? code, int? DiscountPercent);
        Task<ApiResult<object>> AddVoucherAsync(CreateVoucherModelView model);
        Task<ApiResult<object>> UpdateVoucherAsync(int id, UpdateVoucherModelView model);
        Task<ApiResult<object>> DeleteVoucherAsync(int id);
        Task<ApiResult<VoucherModelView>> GetVoucherByIdAsync(int id);
    }
}
