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
using TeamUp.Core.Utils.Firebase;
using TeamUp.Core;
using TeamUp.ModelViews.CourtModelViews;
using TeamUp.ModelViews.SportsComplexModelViews;
using Microsoft.EntityFrameworkCore;
using TeamUp.Repositories.Entity;
using static BabyCare.Core.Utils.SystemConstant;
using TeamUp.ModelViews.RatingModelViews;

namespace TeamUp.Services.Service
{
    public class CourtService : ICourtService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IRatingService _ratingService;

        public CourtService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor, IRatingService ratingService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _ratingService = ratingService;
        }

        public async Task<ApiResult<BasePaginatedList<CourtModelView>>> GetAllCourtxAsync(int pageNumber, int pageSize, string? name, decimal? pricePerHour, string? address, int? sportId, string? type)
        {
            var query = _unitOfWork.GetRepository<Court>().Entities
                .Include(c => c.SportsComplex)
                .Where(c => !c.DeletedTime.HasValue && c.Status == PackageStatus.Active);

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(c => c.Name.Contains(name));

            if (!string.IsNullOrWhiteSpace(type))
                query = query.Where(c => c.SportsComplex.Type.Contains(type));

            if (!string.IsNullOrWhiteSpace(address))
                query = query.Where(c => c.SportsComplex.Address.Contains(address));

            if (pricePerHour.HasValue)
                query = query.Where(c => c.PricePerHour <= pricePerHour.Value);

            if (sportId.HasValue)
                query = query.Where(r => r.SportsComplexId == sportId.Value);

            int totalCount = await query.CountAsync();

            var paginatedCourts = await query
                .OrderByDescending(c => c.CreatedTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = _mapper.Map<List<CourtModelView>>(paginatedCourts);

            // Lấy danh sách các OwnerId duy nhất
            var ownerIds = paginatedCourts
                .Select(c => c.SportsComplex.OwnerId)
                .Distinct()
                .ToList();

            // Tạo dictionary chứa rating summary theo OwnerId
            var ratingSummaries = new Dictionary<int, RatingSummaryModelView>();

            foreach (var ownerId in ownerIds)
            {
                var summary = await _ratingService.GetRatingSummaryForUserAsync(ownerId);
                if (summary.IsSuccessed && summary.ResultObj != null)
                {
                    ratingSummaries[ownerId] = summary.ResultObj;
                }
            }

            for (int i = 0; i < result.Count; i++)
            {
                result[i].SportsComplexModelView = _mapper.Map<SportsComplexModelView>(paginatedCourts[i].SportsComplex);

                var ownerId = paginatedCourts[i].SportsComplex.OwnerId;
                if (ratingSummaries.TryGetValue(ownerId, out var summary))
                {
                    result[i].RatingSummaryModelView = summary;
                }
            }

            return new ApiSuccessResult<BasePaginatedList<CourtModelView>>(
                new BasePaginatedList<CourtModelView>(result, totalCount, pageNumber, pageSize));
        }


        public async Task<ApiResult<object>> AddCourtAsync(CreateCourtModelView model)
        {
            var sportsComplex = await _unitOfWork.GetRepository<SportsComplex>().GetByIdAsync(model.SportsComplexId);
            if (sportsComplex == null || sportsComplex.DeletedTime.HasValue)
                return new ApiErrorResult<object>("Không tìm thấy khu thể thao hợp lệ.");

            var newCourt = _mapper.Map<Court>(model);
            newCourt.CreatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");
            newCourt.CreatedTime = DateTime.Now;

            newCourt.ImageUrls = new List<string>();
            foreach (var img in model.ImageUrls)
            {
                string imgUrl = await ImageHelper.Upload(img);
                newCourt.ImageUrls.Add(imgUrl);
            }

            var owner = await _unitOfWork.GetRepository<ApplicationUser>().GetByIdAsync(sportsComplex.OwnerId);
            if (owner.ExpireDate < DateTime.Now || owner.ExpireDate == null)
            {
                newCourt.Status = PackageStatus.InActive;
            } 
            else
            {
                newCourt.Status = PackageStatus.Active;
            }

            await _unitOfWork.GetRepository<Court>().InsertAsync(newCourt);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Thêm sân thành công.");
        }

        public async Task<ApiResult<object>> UpdateCourtAsync(int id, UpdateCourtModelView model)
        {
            var courtRepo = _unitOfWork.GetRepository<Court>();
            var court = await courtRepo.Entities.FirstOrDefaultAsync(x => x.Id == id && !x.DeletedTime.HasValue);

            if (court == null)
                return new ApiErrorResult<object>("Không tìm thấy sân.");

            if (!string.IsNullOrWhiteSpace(model.Name))
                court.Name = model.Name;

            if (!string.IsNullOrWhiteSpace(model.Description))
                court.Description = model.Description;

            if (model.PricePerHour.HasValue)
                court.PricePerHour = model.PricePerHour.Value;

            if (model.SportsComplexId.HasValue)
            {
                var complex = await _unitOfWork.GetRepository<SportsComplex>().GetByIdAsync(model.SportsComplexId.Value);
                if (complex == null || complex.DeletedTime.HasValue)
                    return new ApiErrorResult<object>("Khu thể thao không hợp lệ.");
                court.SportsComplexId = model.SportsComplexId.Value;
            }

            if (model.ImageUrls != null && model.ImageUrls.Any())
            {
                court.ImageUrls.Clear();
                foreach (var img in model.ImageUrls)
                {
                    var url = await ImageHelper.Upload(img);
                    court.ImageUrls.Add(url);
                }
            }

            var owner = await _unitOfWork.GetRepository<ApplicationUser>().GetByIdAsync(court.SportsComplex.OwnerId);
            if (owner.ExpireDate < DateTime.Now || owner.ExpireDate == null)
            {
                court.Status = PackageStatus.InActive;
            }
            else
            {
                court.Status = PackageStatus.Active;
            }

            court.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");
            court.LastUpdatedTime = DateTime.Now;

            await courtRepo.UpdateAsync(court);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Cập nhật sân thành công.");
        }

        public async Task<ApiResult<object>> DeleteCourtAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Court>();
            var court = await repo.Entities.FirstOrDefaultAsync(x => x.Id == id && !x.DeletedTime.HasValue);

            if (court == null)
                return new ApiErrorResult<object>("Không tìm thấy sân.");

            court.DeletedTime = DateTime.Now;
            court.DeletedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            await repo.UpdateAsync(court);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Xóa sân thành công.");
        }

        public async Task<ApiResult<CourtModelView>> GetCourtByIdAsync(int id)
        {
            var court = await _unitOfWork.GetRepository<Court>().Entities
                .Include(c => c.SportsComplex)
                .FirstOrDefaultAsync(c => c.Id == id && !c.DeletedTime.HasValue && c.Status == PackageStatus.Active);

            if (court == null)
                return new ApiErrorResult<CourtModelView>("Không tìm thấy sân.");

            var result = _mapper.Map<CourtModelView>(court);
            result.SportsComplexModelView = _mapper.Map<SportsComplexModelView>(court.SportsComplex);

            // Thêm RatingSummaryModelView
            var ratingSummaryResult = await _ratingService.GetRatingSummaryForUserAsync(court.SportsComplex.OwnerId);
            if (ratingSummaryResult.IsSuccessed && ratingSummaryResult.ResultObj != null)
            {
                result.RatingSummaryModelView = ratingSummaryResult.ResultObj;
            }

            return new ApiSuccessResult<CourtModelView>(result);
        }


        public async Task<ApiResult<List<CourtModelView>>> GetAllCourt()
        {
            var courts = await _unitOfWork.GetRepository<Court>().Entities
                .Include(c => c.SportsComplex)
                .OrderByDescending(cb => cb.CreatedTime)
                .Where(c => !c.DeletedTime.HasValue && c.Status == PackageStatus.Active)
                .ToListAsync();

            var result = _mapper.Map<List<CourtModelView>>(courts);

            // Lấy danh sách các OwnerId duy nhất
            var ownerIds = courts
                .Select(c => c.SportsComplex.OwnerId)
                .Distinct()
                .ToList();

            // Tạo dictionary chứa rating summary theo OwnerId
            var ratingSummaries = new Dictionary<int, RatingSummaryModelView>();

            foreach (var ownerId in ownerIds)
            {
                var summary = await _ratingService.GetRatingSummaryForUserAsync(ownerId);
                if (summary.IsSuccessed && summary.ResultObj != null)
                {
                    ratingSummaries[ownerId] = summary.ResultObj;
                }
            }

            for (int i = 0; i < result.Count; i++)
            {
                result[i].SportsComplexModelView = _mapper.Map<SportsComplexModelView>(courts[i].SportsComplex);

                var ownerId = courts[i].SportsComplex.OwnerId;
                if (ratingSummaries.TryGetValue(ownerId, out var summary))
                {
                    result[i].RatingSummaryModelView = summary;
                }
            }

            return new ApiSuccessResult<List<CourtModelView>>(result);
        }
    }

}
