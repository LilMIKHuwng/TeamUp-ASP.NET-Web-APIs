using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Core.APIResponse;
using TeamUp.Core;
using TeamUp.ModelViews.CourtModelViews;
using TeamUp.ModelViews.CourtBookingModelViews;

namespace TeamUp.Contract.Services.Interface
{
    public interface ICourtBookingService
    {
        Task<ApiResult<BasePaginatedList<CourtBookingModelView>>> GetAllCourtBookingAsync(int pageNumber, int pageSize, int? userId, int? courtId, DateTime? startTime, DateTime? endTime, string? status);
        Task<ApiResult<object>> AddCourtBookingAsync(CreateCourtBookingModelView model);
        Task<ApiResult<object>> UpdateCourtBookingAsync(int id, UpdateCourtBookingModelView model);
        Task<ApiResult<object>> DeleteCourtBookingAsync(int id);
        Task<ApiResult<CourtBookingModelView>> GetCourtBookingByIdAsync(int id);
        Task<ApiResult<List<CourtBookingModelView>>> GetAllCourtBooking();
        Task<ApiResult<object>> UpdateCourtBookingStatusAsync(int id, string? status, string? paymentStatus);
        Task<ApiResult<object>> GetTotalPriceInMonthForOwnerAndAdmin(int courtId, string paymentMetod, int month, int year);
        Task<ApiResult<List<object>>> GetHourFreeInCourt(int courtId, DateTime startDate);
        Task<ApiResult<object>> GetCourtBookingStatsByOwnerAsync(int ownerId);
        Task<ApiResult<object>> GetMostBookedCourtByOwnerAsync(int ownerId);
        Task<ApiResult<List<object>>> GetBookedSlotsThisWeekByCourtAsync(int courtId);
    }
}
