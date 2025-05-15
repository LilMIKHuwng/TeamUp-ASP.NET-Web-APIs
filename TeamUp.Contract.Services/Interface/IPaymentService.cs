using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Core;
using TeamUp.Core.APIResponse;
using TeamUp.ModelViews.PaymentModelViews;

namespace TeamUp.Contract.Services.Interface
{
    public interface IPaymentService
    {
        Task<ApiResult<string>> CreateVNPayPaymentUrlAsync(CreatePaymentModelView model, string ipAddress);
        Task<ApiResult<object>> HandleVNPayReturnAsync(IQueryCollection vnpParams);
        Task<ApiResult<PaymentModelView>> GetPaymentByIdAsync(int id);
        Task<ApiResult<BasePaginatedList<PaymentModelView>>> GetAllPaymentsAsync(int pageNumber, int pageSize, int? userId);
    }
}
