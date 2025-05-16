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
using TeamUp.ModelViews.RatingModelViews;
using TeamUp.Repositories.Entity;
using Microsoft.EntityFrameworkCore;
using TeamUp.ModelViews.CourtModelViews;
using TeamUp.ModelViews.SportsComplexModelViews;
using TeamUp.ModelViews.UserModelViews.Response;

namespace TeamUp.Services.Service
{
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public RatingService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<ApiResult<BasePaginatedList<RatingModelView>>> GetAllRatingAsync(int pageNumber, int pageSize, int? reviewerId, int? revieweeId, int? ratingValue, string? comment)
        {
            var query = _unitOfWork.GetRepository<Rating>().Entities
                .Include(r => r.Reviewer)
                .Include(r => r.Reviewee)
                .Where(r => !r.DeletedTime.HasValue);

            if (reviewerId.HasValue)
                query = query.Where(r => r.ReviewerId == reviewerId.Value);

            if (revieweeId.HasValue)
                query = query.Where(r => r.RevieweeId == revieweeId.Value);

            if (ratingValue.HasValue)
                query = query.Where(r => r.RatingValue == ratingValue.Value);

            if (!string.IsNullOrWhiteSpace(comment))
                query = query.Where(r => r.Comment!.Contains(comment));

            int totalCount = await query.CountAsync();

            var ratings = await query
                .OrderByDescending(r => r.CreatedTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = _mapper.Map<List<RatingModelView>>(ratings);

            for (int i = 0; i < result.Count; i++)
            {
                result[i].Reviewer = _mapper.Map<UserResponseModel>(ratings[i].Reviewer);

                result[i].Reviewee = _mapper.Map<UserResponseModel>(ratings[i].Reviewee);
            }

            return new ApiSuccessResult<BasePaginatedList<RatingModelView>>(new BasePaginatedList<RatingModelView>(result, totalCount, pageNumber, pageSize));
        }

        public async Task<ApiResult<object>> AddRatingAsync(CreateRatingModelView model)
        {
            if (model.ReviewerId == model.RevieweeId)
                return new ApiErrorResult<object>("Người đánh giá và người được đánh giá không thể là cùng một người.");

            var reviewer = await _unitOfWork.GetRepository<ApplicationUser>().GetByIdAsync(model.ReviewerId);
            var reviewee = await _unitOfWork.GetRepository<ApplicationUser>().GetByIdAsync(model.RevieweeId);

            if (reviewer == null || reviewee == null)
                return new ApiErrorResult<object>("Người đánh giá hoặc người được đánh giá không tồn tại.");

            var rating = _mapper.Map<Rating>(model);
            rating.CreatedBy = model.ReviewerId;
            rating.CreatedTime = DateTime.Now;

            await _unitOfWork.GetRepository<Rating>().InsertAsync(rating);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Thêm đánh giá thành công.");
        }

        public async Task<ApiResult<object>> UpdateRatingAsync(int id, UpdateRatingModelView model)
        {
            var repo = _unitOfWork.GetRepository<Rating>();
            var rating = await repo.Entities.FirstOrDefaultAsync(r => r.Id == id && !r.DeletedTime.HasValue);

            if (rating == null)
                return new ApiErrorResult<object>("Không tìm thấy đánh giá.");

            if (model.ReviewerId.HasValue && model.ReviewerId.Value != rating.ReviewerId)
            {
                var reviewer = await _unitOfWork.GetRepository<ApplicationUser>().GetByIdAsync(model.ReviewerId.Value);
                if (reviewer == null)
                    return new ApiErrorResult<object>("Người đánh giá không tồn tại.");
                rating.ReviewerId = model.ReviewerId.Value;
            }

            if (model.RevieweeId.HasValue && model.RevieweeId.Value != rating.RevieweeId)
            {
                var reviewee = await _unitOfWork.GetRepository<ApplicationUser>().GetByIdAsync(model.RevieweeId.Value);
                if (reviewee == null)
                    return new ApiErrorResult<object>("Người được đánh giá không tồn tại.");
                rating.RevieweeId = model.RevieweeId.Value;
            }

            if (model.RatingValue.HasValue)
                rating.RatingValue = model.RatingValue.Value;

            if (model.Comment != null)
                rating.Comment = model.Comment;

            rating.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");
            rating.LastUpdatedTime = DateTime.Now;

            await repo.UpdateAsync(rating);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Cập nhật đánh giá thành công.");
        }

        public async Task<ApiResult<object>> DeleteRatingAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Rating>();
            var rating = await repo.Entities.FirstOrDefaultAsync(r => r.Id == id && !r.DeletedTime.HasValue);

            if (rating == null)
                return new ApiErrorResult<object>("Không tìm thấy đánh giá.");

            rating.DeletedTime = DateTime.Now;
            rating.DeletedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            await repo.UpdateAsync(rating);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Xóa đánh giá thành công.");
        }

        public async Task<ApiResult<RatingModelView>> GetRatingByIdAsync(int id)
        {
            var rating = await _unitOfWork.GetRepository<Rating>().Entities
                .Include(r => r.Reviewer)
                .Include(r => r.Reviewee)
                .FirstOrDefaultAsync(r => r.Id == id && !r.DeletedTime.HasValue);

            if (rating == null)
                return new ApiErrorResult<RatingModelView>("Không tìm thấy đánh giá.");

            var result = _mapper.Map<RatingModelView>(rating);

            result.Reviewer = _mapper.Map<UserResponseModel>(rating.Reviewer);

            result.Reviewee = _mapper.Map<UserResponseModel>(rating.Reviewee);

            return new ApiSuccessResult<RatingModelView>(result);
        }

        public async Task<ApiResult<List<RatingModelView>>> GetAllRating()
        {
            var ratings = await _unitOfWork.GetRepository<Rating>().Entities
                .Include(r => r.Reviewer)
                .Include(r => r.Reviewee)
                .Where(r => !r.DeletedTime.HasValue)
                .OrderByDescending(r => r.CreatedTime)
                .ToListAsync();

            var result = _mapper.Map<List<RatingModelView>>(ratings);

            for (int i = 0; i < result.Count; i++)
            {
                result[i].Reviewer = _mapper.Map<UserResponseModel>(ratings[i].Reviewer);

                result[i].Reviewee = _mapper.Map<UserResponseModel>(ratings[i].Reviewee);
            }

            return new ApiSuccessResult<List<RatingModelView>>(result);
        }

        public async Task<ApiResult<double>> GetAverageRatingForUserAsync(int userId)
        {
            var ratings = await _unitOfWork.GetRepository<Rating>().Entities
                .Where(r => r.RevieweeId == userId && !r.DeletedTime.HasValue)
                .ToListAsync();

            if (!ratings.Any())
                return new ApiSuccessResult<double>(0);

            var average = ratings.Average(r => r.RatingValue);
            return new ApiSuccessResult<double>(Math.Round(average, 2));
        }

        public async Task<ApiResult<Dictionary<int, int>>> GetRatingStatsForUserAsync(int userId)
        {
            var ratings = await _unitOfWork.GetRepository<Rating>().Entities
                .Where(r => r.RevieweeId == userId && !r.DeletedTime.HasValue)
                .GroupBy(r => r.RatingValue)
                .Select(g => new { Rating = g.Key, Count = g.Count() })
                .ToListAsync();

            var result = Enumerable.Range(1, 5)
                .ToDictionary(star => star, star => ratings.FirstOrDefault(r => r.Rating == star)?.Count ?? 0);

            return new ApiSuccessResult<Dictionary<int, int>>(result);
        }

        public async Task<ApiResult<bool>> CheckIfUserAlreadyRatedAsync(int reviewerId, int revieweeId)
        {
            var exists = await _unitOfWork.GetRepository<Rating>().Entities
                .AnyAsync(r => r.ReviewerId == reviewerId && r.RevieweeId == revieweeId && !r.DeletedTime.HasValue);

            return new ApiSuccessResult<bool>(exists);
        }

        public async Task<ApiResult<List<RatingModelView>>> GetLatestRatingsForUserAsync(int revieweeId, int take = 5)
        {
            var ratings = await _unitOfWork.GetRepository<Rating>().Entities
                .Include(r => r.Reviewer)
                .Where(r => r.RevieweeId == revieweeId && !r.DeletedTime.HasValue)
                .OrderByDescending(r => r.CreatedTime)
                .Take(take)
                .ToListAsync();

            var result = _mapper.Map<List<RatingModelView>>(ratings);

            for (int i = 0; i < result.Count; i++)
            {
                result[i].Reviewer = _mapper.Map<UserResponseModel>(ratings[i].Reviewer);

                result[i].Reviewee = _mapper.Map<UserResponseModel>(ratings[i].Reviewee);
            }

            return new ApiSuccessResult<List<RatingModelView>>(result);
        }

        public async Task<ApiResult<int>> GetTotalReviewerCountForUserAsync(int revieweeId)
        {
            var count = await _unitOfWork.GetRepository<Rating>()
                .Entities
                .Where(r => r.RevieweeId == revieweeId)
                .Select(r => r.ReviewerId)
                .Distinct()
                .CountAsync();

            return new ApiSuccessResult<int>(count);
        }
    }
}
