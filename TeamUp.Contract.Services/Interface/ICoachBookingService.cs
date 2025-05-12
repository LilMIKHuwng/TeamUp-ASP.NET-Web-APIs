using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Core.APIResponse;
using TeamUp.Core;
using TeamUp.ModelViews.CourtBookingModelViews;
using TeamUp.ModelViews.CoachBookingModelViews;

namespace TeamUp.Contract.Services.Interface
{
    public interface ICoachBookingService
    {
        Task<ApiResult<BasePaginatedList<CoachBookingModelView>>> GetAllCoachBookingAsync(int pageNumber, int pageSize,int? coachId, int? userId, int? courtId, TimeSpan? startTime, TimeSpan? endTime, string? status);
        Task<ApiResult<object>> AddCoachBookingAsync(CreateCoachBookingModelView model);
        Task<ApiResult<object>> UpdateCoachBookingAsync(int id, UpdateCoachBookingModelView model);
        Task<ApiResult<object>> DeleteCoachBookingAsync(int id);
        Task<ApiResult<CoachBookingModelView>> GetCoachBookingByIdAsync(int id);
        Task<ApiResult<List<CoachBookingModelView>>> GetAllCoachBooking();
        Task<ApiResult<object>> UpdateCoachBookingStatusAsync(int id, string? status, string? paymentStatus);
    }
}
