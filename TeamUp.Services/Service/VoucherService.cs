using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.Contract.Repositories.Interface;
using TeamUp.Contract.Services.Interface;
using TeamUp.Core.APIResponse;
using TeamUp.ModelViews.VoucherModelViews;
using TeamUp.Core;

namespace TeamUp.Services.Service
{
    public class VoucherService : IVoucherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public VoucherService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<ApiResult<BasePaginatedList<VoucherModelView>>> GetAllVoucherAsync(int pageNumber, int pageSize, string? code, int? discountPercent)
        {
            var query = _unitOfWork.GetRepository<Voucher>().Entities
                .Where(v => !v.DeletedTime.HasValue)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(code))
                query = query.Where(v => v.Code.Contains(code));

            if (discountPercent.HasValue)
                query = query.Where(v => v.DiscountPercent == discountPercent.Value);

            int totalCount = await query.CountAsync();

            var vouchers = await query
                .OrderByDescending(v => v.CreatedTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var viewModels = _mapper.Map<List<VoucherModelView>>(vouchers);
            var result = new BasePaginatedList<VoucherModelView>(viewModels, totalCount, pageNumber, pageSize);

            return new ApiSuccessResult<BasePaginatedList<VoucherModelView>>(result);
        }

        public async Task<ApiResult<object>> AddVoucherAsync(CreateVoucherModelView model)
        {
            var existed = await _unitOfWork.GetRepository<Voucher>().Entities
                .AnyAsync(v => v.Code == model.Code && !v.DeletedTime.HasValue);

            if (existed)
                return new ApiErrorResult<object>("Voucher code already exists.");

            var entity = _mapper.Map<Voucher>(model);
            entity.CreatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");
            entity.CreatedTime = DateTime.Now;

            await _unitOfWork.GetRepository<Voucher>().InsertAsync(entity);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Voucher added successfully.");
        }

        public async Task<ApiResult<object>> UpdateVoucherAsync(int id, UpdateVoucherModelView model)
        {
            var voucher = await _unitOfWork.GetRepository<Voucher>().Entities
                .FirstOrDefaultAsync(v => v.Id == id && !v.DeletedTime.HasValue);

            if (voucher == null)
                return new ApiErrorResult<object>("Voucher not found.");

            bool isUpdated = false;

            if (!string.IsNullOrWhiteSpace(model.Code) && model.Code != voucher.Code)
            {
                bool codeExists = await _unitOfWork.GetRepository<Voucher>().Entities
                    .AnyAsync(v => v.Code == model.Code && v.Id != id && !v.DeletedTime.HasValue);

                if (codeExists)
                    return new ApiErrorResult<object>("Another voucher with the same code already exists.");

                voucher.Code = model.Code;
                isUpdated = true;
            }

            if (!string.IsNullOrWhiteSpace(model.Description) && model.Description != voucher.Description)
            {
                voucher.Description = model.Description;
                isUpdated = true;
            }

            if (model.DiscountPercent.HasValue && model.DiscountPercent.Value != voucher.DiscountPercent)
            {
                voucher.DiscountPercent = model.DiscountPercent.Value;
                isUpdated = true;
            }

            if (isUpdated)
            {
                voucher.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");
                voucher.LastUpdatedTime = DateTime.Now;

                await _unitOfWork.GetRepository<Voucher>().UpdateAsync(voucher);
                await _unitOfWork.SaveAsync();
            }

            return new ApiSuccessResult<object>("Voucher updated successfully.");
        }

        public async Task<ApiResult<object>> DeleteVoucherAsync(int id)
        {
            var voucher = await _unitOfWork.GetRepository<Voucher>().Entities
                .FirstOrDefaultAsync(v => v.Id == id && !v.DeletedTime.HasValue);

            if (voucher == null)
                return new ApiErrorResult<object>("Voucher not found.");

            voucher.DeletedTime = DateTime.Now;
            voucher.DeletedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            await _unitOfWork.GetRepository<Voucher>().UpdateAsync(voucher);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Voucher deleted successfully.");
        }

        public async Task<ApiResult<VoucherModelView>> GetVoucherByIdAsync(int id)
        {
            var voucher = await _unitOfWork.GetRepository<Voucher>().Entities
                .FirstOrDefaultAsync(v => v.Id == id && !v.DeletedTime.HasValue);

            if (voucher == null)
                return new ApiErrorResult<VoucherModelView>("Voucher not found.");

            var result = _mapper.Map<VoucherModelView>(voucher);
            return new ApiSuccessResult<VoucherModelView>(result);
        }
    }
}
