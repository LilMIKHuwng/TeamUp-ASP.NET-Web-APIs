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
using TeamUp.Core.Base;
using TeamUp.Core.Utils.Firebase;
using TeamUp.Core;
using TeamUp.ModelViews.SportsComplexModelViews;
using Microsoft.EntityFrameworkCore;
using TeamUp.ModelViews.UserModelViews.Response;
using BabyCare.Core.Utils;
using static BabyCare.Core.Utils.SystemConstant;
using TeamUp.Repositories.Entity;
using TeamUp.ModelViews.RatingModelViews;

namespace TeamUp.Services.Service
{
    public class SportsComplexService : ISportsComplexService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IRatingService _ratingService;

        public SportsComplexService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor, IRatingService ratingService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _ratingService = ratingService;
        }

        public async Task<ApiResult<BasePaginatedList<SportsComplexModelView>>> GetAllSportsComplexAsync(int pageNumber, int pageSize, string? name, string? address)
        {
            IQueryable<SportsComplex> query = _unitOfWork.GetRepository<SportsComplex>().Entities
                .Include(sc => sc.Owner)
                .Where(sc => !sc.DeletedTime.HasValue && sc.Status == PackageStatus.Active);

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(sc => sc.Name.Contains(name));

            if (!string.IsNullOrWhiteSpace(address))
                query = query.Where(sc => sc.Address.Contains(address));

            query = query.OrderByDescending(sc => sc.CreatedTime);

            int totalCount = await query.CountAsync();

            List<SportsComplex> paginatedComplexes = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            List<SportsComplexModelView> complexModelViews = _mapper.Map<List<SportsComplexModelView>>(paginatedComplexes);

            // Lấy danh sách ownerId duy nhất
            var ownerIds = paginatedComplexes.Select(sc => sc.OwnerId).Distinct();

            // Gọi rating service cho từng owner
            var ratingDict = new Dictionary<int, RatingSummaryModelView>();
            foreach (var ownerId in ownerIds)
            {
                var ratingResult = await _ratingService.GetRatingSummaryForUserAsync(ownerId);
                if (ratingResult.IsSuccessed)
                {
                    ratingDict[ownerId] = ratingResult.ResultObj;
                }
            }

            for (int i = 0; i < complexModelViews.Count; i++)
            {
                var originalEntity = paginatedComplexes[i];
                if (originalEntity.Owner != null)
                {
                    complexModelViews[i].Owner = _mapper.Map<EmployeeResponseModel>(originalEntity.Owner);
                }

                var ownerId = originalEntity.OwnerId;
                complexModelViews[i].RatingSummaryModelView = ratingDict.ContainsKey(ownerId)
                    ? ratingDict[ownerId]
                    : new RatingSummaryModelView { AverageRating = 0, TotalReviewerCount = 0 };
            }

            var result = new BasePaginatedList<SportsComplexModelView>(complexModelViews, totalCount, pageNumber, pageSize);
            return new ApiSuccessResult<BasePaginatedList<SportsComplexModelView>>(result);
        }


        public async Task<ApiResult<object>> AddSportsComplexAsync(CreateSportsComplexModelView model)
        {
            var repo = _unitOfWork.GetRepository<SportsComplex>();

            var newComplex = _mapper.Map<SportsComplex>(model);
            newComplex.CreatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");
            newComplex.CreatedTime = DateTime.Now;

            var validTypes = new[]
            {
                SystemConstant.Type.Soccer,
                SystemConstant.Type.Badminton,
                SystemConstant.Type.PickleBall
            };

            if (!validTypes.Contains(model.Type))
                return new ApiErrorResult<object>("Loại thể thao không hợp lệ.");

            newComplex.ImageUrls = new List<string>();
            foreach (var img in model.ImageUrls)
            {
                string imgUrl = await ImageHelper.Upload(img);
                newComplex.ImageUrls.Add(imgUrl);
            }

            var owner = await _unitOfWork.GetRepository<ApplicationUser>().GetByIdAsync(model.OwnerId);
            if (owner.ExpireDate < DateTime.Now || owner.ExpireDate == null)
            {
                newComplex.Status = PackageStatus.InActive;
            }
            else
            {
                newComplex.Status = PackageStatus.Active;
            }

            await repo.InsertAsync(newComplex);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Thêm khu thể thao thành công.");
        }

        public async Task<ApiResult<object>> UpdateSportsComplexAsync(int id, UpdateSportsComplexModelView model)
        {
            var repo = _unitOfWork.GetRepository<SportsComplex>();
            var complex = await repo.Entities
                .FirstOrDefaultAsync(x => x.Id == id && !x.DeletedTime.HasValue);

            if (complex == null)
                return new ApiErrorResult<object>("Không tìm thấy khu thể thao.");

            if (!string.IsNullOrWhiteSpace(model.Name))
                complex.Name = model.Name;

            if (!string.IsNullOrWhiteSpace(model.Address))
                complex.Address = model.Address;

            if (model.OwnerId.HasValue)
                complex.OwnerId = model.OwnerId.Value;

            if (!string.IsNullOrWhiteSpace(model.Type))
            {
                var validTypes = new[]
                {
                    SystemConstant.Type.Soccer,
                    SystemConstant.Type.Badminton,
                    SystemConstant.Type.PickleBall
                };

                if (!validTypes.Contains(model.Type))
                    return new ApiErrorResult<object>("Loại thể thao không hợp lệ.");

                complex.Type = model.Type;
            }

            if (model.ImageUrls != null && model.ImageUrls.Any())
            {
                complex.ImageUrls.Clear(); 
                foreach (var img in model.ImageUrls)
                {
                    var url = await ImageHelper.Upload(img);
                    complex.ImageUrls.Add(url);
                }
            }

            var owner = await _unitOfWork.GetRepository<ApplicationUser>().GetByIdAsync(complex.OwnerId);
            if (owner.ExpireDate < DateTime.Now || owner.ExpireDate == null)
            {
                complex.Status = PackageStatus.InActive;
            }
            else
            {
                complex.Status = PackageStatus.Active;
            }

            complex.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");
            complex.LastUpdatedTime = DateTime.Now;

            await repo.UpdateAsync(complex);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Cập nhật khu thể thao thành công.");
        }

        public async Task<ApiResult<object>> DeleteSportsComplexAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<SportsComplex>();
            var complex = await repo.Entities.FirstOrDefaultAsync(x => x.Id == id && !x.DeletedTime.HasValue);

            if (complex == null)
                return new ApiErrorResult<object>("Không tìm thấy khu thể thao.");

            complex.DeletedTime = DateTime.Now;
            complex.DeletedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            await repo.UpdateAsync(complex);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Xóa khu thể thao thành công.");
        }

        public async Task<ApiResult<SportsComplexModelView>> GetSportsComplexByIdAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<SportsComplex>();
            var complex = await repo.Entities
                .Include(x => x.Owner)
                .FirstOrDefaultAsync(x => x.Id == id && !x.DeletedTime.HasValue && x.Status == PackageStatus.Active);

            if (complex == null)
                return new ApiErrorResult<SportsComplexModelView>("Không tìm thấy khu thể thao.");

            var result = _mapper.Map<SportsComplexModelView>(complex);

            // Gán thông tin chủ sân
            if (complex.Owner != null)
            {
                result.Owner = _mapper.Map<EmployeeResponseModel>(complex.Owner);

                // Gán RatingSummary nếu Owner tồn tại
                var ratingResult = await _ratingService.GetRatingSummaryForUserAsync(complex.Owner.Id);
                result.RatingSummaryModelView = ratingResult.IsSuccessed
                    ? ratingResult.ResultObj
                    : new RatingSummaryModelView { AverageRating = 0, TotalReviewerCount = 0 };
            }
            else
            {
                result.RatingSummaryModelView = new RatingSummaryModelView { AverageRating = 0, TotalReviewerCount = 0 };
            }

            return new ApiSuccessResult<SportsComplexModelView>(result);
        }


        public async Task<ApiResult<List<SportsComplexModelView>>> GetAllSportsComplex()
        {
            var complexes = await _unitOfWork.GetRepository<SportsComplex>().Entities
                .Include(x => x.Owner)
                .OrderByDescending(r => r.CreatedTime)
                .Where(x => !x.DeletedTime.HasValue && x.Status == PackageStatus.Active)
                .ToListAsync();

            var result = _mapper.Map<List<SportsComplexModelView>>(complexes);

            // Lấy danh sách ownerId duy nhất
            var ownerIds = complexes.Select(sc => sc.OwnerId).Distinct();

            // Gọi rating service cho từng owner
            var ratingDict = new Dictionary<int, RatingSummaryModelView>();
            foreach (var ownerId in ownerIds)
            {
                var ratingResult = await _ratingService.GetRatingSummaryForUserAsync(ownerId);
                if (ratingResult.IsSuccessed)
                {
                    ratingDict[ownerId] = ratingResult.ResultObj;
                }
            }

            for (int i = 0; i < result.Count; i++)
            {
                var originalEntity = complexes[i];
                if (originalEntity.Owner != null)
                {
                    result[i].Owner = _mapper.Map<EmployeeResponseModel>(originalEntity.Owner);
                }

                var ownerId = originalEntity.OwnerId;
                result[i].RatingSummaryModelView = ratingDict.ContainsKey(ownerId)
                    ? ratingDict[ownerId]
                    : new RatingSummaryModelView { AverageRating = 0, TotalReviewerCount = 0 };
            }

            return new ApiSuccessResult<List<SportsComplexModelView>>(result);
        }
    }
}
