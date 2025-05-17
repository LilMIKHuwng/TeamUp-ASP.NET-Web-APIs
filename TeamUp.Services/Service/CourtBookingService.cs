using AutoMapper;
using BabyCare.Core.Utils;
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
using TeamUp.ModelViews.CourtBookingModelViews;
using TeamUp.Repositories.Entity;
using Microsoft.EntityFrameworkCore;
using TeamUp.ModelViews.RoomModelViews;
using TeamUp.ModelViews.UserModelViews.Response;
using TeamUp.ModelViews.CourtModelViews;
using static BabyCare.Core.Utils.SystemConstant;
using TeamUp.ModelViews.SportsComplexModelViews;
using System.Globalization;

namespace TeamUp.Services.Service
{
    public class CourtBookingService : ICourtBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public CourtBookingService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<ApiResult<BasePaginatedList<CourtBookingModelView>>> GetAllCourtBookingAsync(
            int pageNumber,
            int pageSize,
            int? userId,
            int? courtId,
            DateTime? startTime,
            DateTime? endTime,
            string? status)
        {
            var query = _unitOfWork.GetRepository<CourtBooking>().Entities
                .Include(cb => cb.Court)
                .Include(cb => cb.User)
                .Where(cb => !cb.DeletedTime.HasValue);

            if (userId.HasValue)
            {
                query = query.Where(cb => cb.UserId == userId.Value);
            }

            if (courtId.HasValue)
            {
                query = query.Where(cb => cb.CourtId == courtId.Value);
            }

            if (startTime.HasValue)
            {
                query = query.Where(cb => cb.StartTime >= startTime.Value);
            }

            if (endTime.HasValue)
            {
                query = query.Where(cb => cb.EndTime <= endTime.Value);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(cb => cb.Status == status);
            }

            int totalCount = await query.CountAsync();

            var bookings = await query
                .OrderByDescending(cb => cb.CreatedTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = _mapper.Map<List<CourtBookingModelView>>(bookings);

            for (int i = 0; i < result.Count; i++)
            {
                result[i].User = _mapper.Map<UserResponseModel>(bookings[i].User);

                result[i].Court = _mapper.Map<CourtModelView>(bookings[i].Court);

                result[i].Court.SportsComplexModelView = _mapper.Map<SportsComplexModelView>(bookings[i].Court.SportsComplex);
            }

            return new ApiSuccessResult<BasePaginatedList<CourtBookingModelView>>(
                new BasePaginatedList<CourtBookingModelView>(result, totalCount, pageNumber, pageSize));
        }

        public async Task<ApiResult<List<CourtBookingModelView>>> GetAllCourtBooking()
        {
            var bookings = await _unitOfWork.GetRepository<CourtBooking>().Entities
                .Include(cb => cb.Court)
                .Include(cb => cb.User)
                .OrderByDescending(cb => cb.CreatedTime)
                .Where(cb => !cb.DeletedTime.HasValue)
                .ToListAsync();

            var result = _mapper.Map<List<CourtBookingModelView>>(bookings);

            for (int i = 0; i < result.Count; i++)
            {
                result[i].User = _mapper.Map<UserResponseModel>(bookings[i].User);

                result[i].Court = _mapper.Map<CourtModelView>(bookings[i].Court);

                result[i].Court.SportsComplexModelView = _mapper.Map<SportsComplexModelView>(bookings[i].Court.SportsComplex);
            }

            return new ApiSuccessResult<List<CourtBookingModelView>>(result);
        }

        public async Task<ApiResult<CourtBookingModelView>> GetCourtBookingByIdAsync(int id)
        {
            var booking = await _unitOfWork.GetRepository<CourtBooking>().Entities
                .Include(cb => cb.Court)
                .Include(cb => cb.User)
                .FirstOrDefaultAsync(cb => cb.Id == id && !cb.DeletedTime.HasValue);

            if (booking == null)
                return new ApiErrorResult<CourtBookingModelView>("Không tìm thấy lịch đặt sân.");

            var result = _mapper.Map<CourtBookingModelView>(booking);

            result.User = _mapper.Map<UserResponseModel>(booking.User);

            result.Court = _mapper.Map<CourtModelView>(booking.Court);

            result.Court.SportsComplexModelView = _mapper.Map<SportsComplexModelView>(booking.Court.SportsComplex);

            return new ApiSuccessResult<CourtBookingModelView>(result);
        }

        public async Task<ApiResult<object>> AddCourtBookingAsync(CreateCourtBookingModelView model)
        {
            var court = await _unitOfWork.GetRepository<Court>().GetByIdAsync(model.CourtId);
            var user = await _unitOfWork.GetRepository<ApplicationUser>().GetByIdAsync(model.UserId);

            if (court == null || user == null)
                return new ApiErrorResult<object>("Dữ liệu sân hoặc người dùng không hợp lệ.");

            // 1. Kiểm tra trùng lịch trong CourtBooking
            bool isCourtBookingConflict = await _unitOfWork.GetRepository<CourtBooking>().Entities
                .AnyAsync(b =>
                    b.CourtId == model.CourtId &&
                    !b.DeletedTime.HasValue &&
                    (
                        (model.StartTime >= b.StartTime && model.StartTime < b.EndTime) ||
                        (model.EndTime > b.StartTime && model.EndTime <= b.EndTime) ||
                        (model.StartTime <= b.StartTime && model.EndTime >= b.EndTime)
                    )
                );

            if (isCourtBookingConflict)
                return new ApiErrorResult<object>("Khung giờ bạn chọn đã có người đặt sân.");

            // 2. Kiểm tra trùng lịch trong CoachBooking (dùng TimeSpan)
            var bookingDate = model.StartTime.Date;
            var courtStartTime = model.StartTime.TimeOfDay;
            var courtEndTime = model.EndTime.TimeOfDay;

            bool isCoachBookingConflict = await _unitOfWork.GetRepository<CoachBooking>().Entities
                .AnyAsync(cb =>
                    cb.CourtId == model.CourtId &&
                    !cb.DeletedTime.HasValue &&
                    cb.SelectedDates.Contains(bookingDate) &&
                    (
                        (courtStartTime >= cb.StartTime && courtStartTime < cb.EndTime) ||
                        (courtEndTime > cb.StartTime && courtEndTime <= cb.EndTime) ||
                        (courtStartTime <= cb.StartTime && courtEndTime >= cb.EndTime)
                    )
                );

            if (isCoachBookingConflict)
                return new ApiErrorResult<object>("Khung giờ bạn chọn đã có lịch huấn luyện viên.");

            // 3. Tạo booking
            var booking = _mapper.Map<CourtBooking>(model);
            booking.CreatedTime = DateTime.Now;
            booking.CreatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");
            booking.TotalPrice = (decimal)(model.EndTime - model.StartTime).TotalHours * court.PricePerHour;

            booking.Status = SystemConstant.BookingStatus.Pending;
            booking.PaymentStatus = SystemConstant.PaymentStatus.Pending;

            await _unitOfWork.GetRepository<CourtBooking>().InsertAsync(booking);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Đặt sân thành công.");
        }


        public async Task<ApiResult<object>> UpdateCourtBookingAsync(int id, UpdateCourtBookingModelView model)
        {
            var repo = _unitOfWork.GetRepository<CourtBooking>();
            var booking = await repo.Entities.FirstOrDefaultAsync(b => b.Id == id && !b.DeletedTime.HasValue);
            if (booking == null)
                return new ApiErrorResult<object>("Không tìm thấy lịch đặt sân.");

            int courtId = model.CourtId ?? booking.CourtId;

            if (model.StartTime.HasValue && model.EndTime.HasValue)
            {
                DateTime startTime = model.StartTime.Value;
                DateTime endTime = model.EndTime.Value;

                // 1. Kiểm tra trùng lịch CourtBooking
                bool isCourtBookingConflict = await _unitOfWork.GetRepository<CourtBooking>().Entities
                    .AnyAsync(b =>
                        b.CourtId == courtId &&
                        b.Id != id &&
                        !b.DeletedTime.HasValue &&
                        (
                            (startTime >= b.StartTime && startTime < b.EndTime) ||
                            (endTime > b.StartTime && endTime <= b.EndTime) ||
                            (startTime <= b.StartTime && endTime >= b.EndTime)
                        )
                    );

                if (isCourtBookingConflict)
                    return new ApiErrorResult<object>("Khung giờ bạn chọn đã có người đặt sân.");

                // 2. Kiểm tra trùng lịch CoachBooking
                var bookingDate = startTime.Date;
                var courtStartTime = startTime.TimeOfDay;
                var courtEndTime = endTime.TimeOfDay;

                bool isCoachBookingConflict = await _unitOfWork.GetRepository<CoachBooking>().Entities
                    .AnyAsync(cb =>
                        cb.CourtId == courtId &&
                        !cb.DeletedTime.HasValue &&
                        cb.SelectedDates.Contains(bookingDate) &&
                        (
                            (courtStartTime >= cb.StartTime && courtStartTime < cb.EndTime) ||
                            (courtEndTime > cb.StartTime && courtEndTime <= cb.EndTime) ||
                            (courtStartTime <= cb.StartTime && courtEndTime >= cb.EndTime)
                        )
                    );

                if (isCoachBookingConflict)
                    return new ApiErrorResult<object>("Khung giờ bạn chọn đã có lịch huấn luyện viên.");

                booking.StartTime = startTime;
                booking.EndTime = endTime;
            }

            if (model.CourtId.HasValue)
                booking.CourtId = model.CourtId.Value;

            if (model.UserId.HasValue)
                booking.UserId = model.UserId.Value;

            if (!string.IsNullOrEmpty(model.Status))
            {
                var allowedStatuses = new[]
                {
                    BookingStatus.Pending, BookingStatus.Confirmed, BookingStatus.InProgress,
                    BookingStatus.Completed, BookingStatus.CancelledByUser, BookingStatus.CancelledByOwner,
                    BookingStatus.NoShow, BookingStatus.Failed, BookingStatus.CancelledByCoach
                };

                if (!allowedStatuses.Contains(model.Status))
                    return new ApiErrorResult<object>("Trạng thái không hợp lệ.");

                booking.Status = model.Status;
            }

            if (!string.IsNullOrEmpty(model.PaymentMethod))
                booking.PaymentMethod = model.PaymentMethod;

            booking.LastUpdatedTime = DateTime.Now;
            booking.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            // Tính lại giá nếu có thời gian hợp lệ
            var court = await _unitOfWork.GetRepository<Court>().GetByIdAsync(booking.CourtId);
            if (court != null && booking.StartTime != default && booking.EndTime != default)
                booking.TotalPrice = (decimal)(booking.EndTime - booking.StartTime).TotalHours * court.PricePerHour;

            await repo.UpdateAsync(booking);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Cập nhật thành công.");
        }



        public async Task<ApiResult<object>> DeleteCourtBookingAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<CourtBooking>();
            var booking = await repo.Entities.FirstOrDefaultAsync(b => b.Id == id && !b.DeletedTime.HasValue);
            if (booking == null)
                return new ApiErrorResult<object>("Không tìm thấy lịch đặt sân.");

            booking.DeletedTime = DateTime.Now;
            booking.DeletedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            booking.Status = BookingStatus.Failed;

            await repo.UpdateAsync(booking);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Xóa thành công.");
        }

        public async Task<ApiResult<object>> UpdateCourtBookingStatusAsync(int id, string? status, string? paymentStatus)
        {
            var repo = _unitOfWork.GetRepository<CourtBooking>();
            var booking = await repo.Entities.FirstOrDefaultAsync(b => b.Id == id && !b.DeletedTime.HasValue);
            if (booking == null)
                return new ApiErrorResult<object>("Không tìm thấy lịch đặt sân.");

            if (!string.IsNullOrWhiteSpace(status))
            {
                var allowedStatuses = new[]
                {
                    BookingStatus.Pending, BookingStatus.Confirmed, BookingStatus.InProgress,
                    BookingStatus.Completed, BookingStatus.CancelledByUser, BookingStatus.CancelledByOwner,
                    BookingStatus.NoShow, BookingStatus.Failed, BookingStatus.CancelledByCoach
                };

                if (!allowedStatuses.Contains(status))
                    return new ApiErrorResult<object>("Trạng thái đặt sân không hợp lệ.");

                booking.Status = status;
            }

            if (!string.IsNullOrWhiteSpace(paymentStatus))
            {
                var allowedPaymentStatuses = new[]
                {
                    PaymentStatus.Pending, PaymentStatus.Paid, PaymentStatus.Failed,
                    PaymentStatus.Refunded, PaymentStatus.Cancelled
                };

                if (!allowedPaymentStatuses.Contains(paymentStatus))
                    return new ApiErrorResult<object>("Trạng thái thanh toán không hợp lệ.");

                booking.PaymentStatus = paymentStatus;
            }

            booking.LastUpdatedTime = DateTimeOffset.UtcNow;
            booking.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            await repo.UpdateAsync(booking);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Cập nhật trạng thái thành công.");
        }

        public async Task<ApiResult<object>> GetTotalPriceInMonthForOwnerAndAdmin(int courtId, string paymentMethod, int month, int year)
        {
            var bookings = await _unitOfWork.GetRepository<CourtBooking>().Entities
                .Include(b => b.Court)
                .Where(b =>
                    b.CourtId == courtId &&
                    !b.DeletedTime.HasValue &&
                    b.Status == BookingStatus.Completed &&
                    b.StartTime.Month == month &&
                    b.StartTime.Year == year && 
                    b.PaymentMethod == paymentMethod)
                .ToListAsync();

            var totalPrice = bookings.Sum(b => b.TotalPrice);

            return new ApiSuccessResult<object>(new
            {
                TotalPrice = totalPrice
            });
        }

        public async Task<ApiResult<List<object>>> GetHourFreeInCourt(int courtId, DateTime startDate)
        {
            var now = DateTime.Now;
            if (startDate.Date < now.Date)
            {
                return new ApiErrorResult<List<object>>("Ngày bắt đầu không được nhỏ hơn ngày hiện tại.");
            }

            var endDate = startDate.Date.AddDays(7);
            var openTime = TimeSpan.FromHours(5);    // 5:00 AM
            var closeTime = TimeSpan.FromHours(23);  // 11:00 PM

            // Lấy booking của sân trong khoảng ngày
            var courtBookings = await _unitOfWork.GetRepository<CourtBooking>().Entities
                .Where(b =>
                    b.CourtId == courtId &&
                    !b.DeletedTime.HasValue &&
                    b.StartTime < endDate &&
                    b.EndTime > startDate)
                .ToListAsync();

            // Lấy coach bookings có courtId tương ứng
            var coachBookings = await _unitOfWork.GetRepository<CoachBooking>().Entities
                .Where(cb =>
                    cb.CourtId == courtId &&
                    !cb.DeletedTime.HasValue)
                .ToListAsync();

            var result = new List<object>();

            for (var date = startDate.Date; date < endDate; date = date.AddDays(1))
            {
                var dailyFreeSlots = new List<object>();

                for (var hour = openTime; hour < closeTime; hour = hour.Add(TimeSpan.FromHours(1)))
                {
                    var slotStart = date.Add(hour);
                    var slotEnd = slotStart.AddHours(1);

                    bool hasCourtBookingConflict = courtBookings.Any(b =>
                        slotStart < b.EndTime && slotEnd > b.StartTime);

                    bool hasCoachBookingConflict = coachBookings.Any(cb =>
                        cb.SelectedDates.Contains(date) &&
                        hour < cb.EndTime && hour.Add(TimeSpan.FromHours(1)) > cb.StartTime);

                    if (!hasCourtBookingConflict && !hasCoachBookingConflict)
                    {
                        dailyFreeSlots.Add(new
                        {
                            StartTime = slotStart.ToString("HH:mm"),
                            EndTime = slotEnd.ToString("HH:mm")
                        });
                    }
                }

                result.Add(new
                {
                    CourtId = courtId,
                    WeekDay = date.ToString("dddd", new CultureInfo("vi-VN")),
                    Date = date.ToString("yyyy-MM-dd"),
                    FreeSlots = dailyFreeSlots
                });
            }

            return new ApiSuccessResult<List<object>>(result);
        }



    }
}
