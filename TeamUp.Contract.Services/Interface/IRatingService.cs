using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Core.APIResponse;
using TeamUp.Core;
using TeamUp.ModelViews.CourtModelViews;
using TeamUp.ModelViews.RatingModelViews;
using System.ComponentModel.DataAnnotations;

namespace TeamUp.Contract.Services.Interface
{
    public interface IRatingService
    {
        Task<ApiResult<BasePaginatedList<RatingModelView>>> GetAllRatingAsync(int pageNumber, int pageSize, int? reviewerId, int? RevieweeId, int? ratingValue, string? Comment);
        Task<ApiResult<object>> AddRatingAsync(CreateRatingModelView model);
        Task<ApiResult<object>> UpdateRatingAsync(int id, UpdateRatingModelView model);
        Task<ApiResult<object>> DeleteRatingAsync(int id);
        Task<ApiResult<RatingModelView>> GetRatingByIdAsync(int id);
        Task<ApiResult<List<RatingModelView>>> GetAllRating();
        Task<ApiResult<double>> GetAverageRatingForUserAsync(int userId);
        Task<ApiResult<Dictionary<int, int>>> GetRatingStatsForUserAsync(int userId);
        Task<ApiResult<bool>> CheckIfUserAlreadyRatedAsync(int reviewerId, int revieweeId);
        Task<ApiResult<List<RatingModelView>>> GetLatestRatingsForUserAsync(int revieweeId, int take = 5);

    }
}
