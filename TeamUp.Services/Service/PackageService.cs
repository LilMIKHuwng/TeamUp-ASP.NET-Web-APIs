using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.Contract.Repositories.Interface;
using TeamUp.Contract.Services.Interface;
using TeamUp.Core.APIResponse;
using TeamUp.Core;
using TeamUp.ModelViews.PackageModelViews;
using Microsoft.EntityFrameworkCore;

namespace TeamUp.Services.Service
{
    public class PackageService : IPackageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public PackageService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<ApiResult<BasePaginatedList<PackageModelView>>> GetAllPackageAsync(int pageNumber, int pageSize, string? name, string? type, decimal? price, int? durationDays)
        {
            var query = _unitOfWork.GetRepository<Package>().Entities
                .Where(p => !p.DeletedTime.HasValue);

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(p => p.Name.Contains(name));

            if (!string.IsNullOrWhiteSpace(type))
                query = query.Where(p => p.Type == type);

            if (price.HasValue)
                query = query.Where(p => p.Price == price.Value);

            if (durationDays.HasValue)
                query = query.Where(p => p.DurationDays == durationDays.Value);

            int totalCount = await query.CountAsync();

            var pagedPackages = await query
                .OrderByDescending(p => p.CreatedTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = _mapper.Map<List<PackageModelView>>(pagedPackages);

            return new ApiSuccessResult<BasePaginatedList<PackageModelView>>(
                new BasePaginatedList<PackageModelView>(result, totalCount, pageNumber, pageSize));
        }

        public async Task<ApiResult<object>> AddPackageAsync(CreatePackageModelView model)
        {
            var newPackage = _mapper.Map<Package>(model);
            newPackage.CreatedTime = DateTime.Now;
            newPackage.CreatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            await _unitOfWork.GetRepository<Package>().InsertAsync(newPackage);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Thêm gói thành công.");
        }

        public async Task<ApiResult<object>> UpdatePackageAsync(int id, UpdatePackageModelView model)
        {
            var packageRepo = _unitOfWork.GetRepository<Package>();
            var package = await packageRepo.Entities.FirstOrDefaultAsync(p => p.Id == id && !p.DeletedTime.HasValue);

            if (package == null)
                return new ApiErrorResult<object>("Không tìm thấy gói.");

            if (!string.IsNullOrWhiteSpace(model.Name))
                package.Name = model.Name;

            if (model.Price.HasValue)
                package.Price = model.Price.Value;

            if (!string.IsNullOrWhiteSpace(model.Description))
                package.Description = model.Description;

            if (model.DurationDays.HasValue)
                package.DurationDays = model.DurationDays.Value;

            if (!string.IsNullOrWhiteSpace(model.Type))
                package.Type = model.Type;

            package.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");
            package.LastUpdatedTime = DateTime.Now;

            await packageRepo.UpdateAsync(package);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Cập nhật gói thành công.");
        }

        public async Task<ApiResult<object>> DeletePackageAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Package>();
            var package = await repo.Entities.FirstOrDefaultAsync(p => p.Id == id && !p.DeletedTime.HasValue);

            if (package == null)
                return new ApiErrorResult<object>("Không tìm thấy gói.");

            package.DeletedTime = DateTime.Now;
            package.DeletedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            await repo.UpdateAsync(package);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Xóa gói thành công.");
        }

        public async Task<ApiResult<PackageModelView>> GetPackageByIdAsync(int id)
        {
            var package = await _unitOfWork.GetRepository<Package>().Entities
                .FirstOrDefaultAsync(p => p.Id == id && !p.DeletedTime.HasValue);

            if (package == null)
                return new ApiErrorResult<PackageModelView>("Không tìm thấy gói.");

            var result = _mapper.Map<PackageModelView>(package);
            return new ApiSuccessResult<PackageModelView>(result);
        }

        public async Task<ApiResult<List<PackageModelView>>> GetAllPackage()
        {
            var packages = await _unitOfWork.GetRepository<Package>().Entities
                .Where(p => !p.DeletedTime.HasValue)
                .OrderByDescending(p => p.CreatedTime)
                .ToListAsync();

            var result = _mapper.Map<List<PackageModelView>>(packages);
            return new ApiSuccessResult<List<PackageModelView>>(result);
        }
    }
}
