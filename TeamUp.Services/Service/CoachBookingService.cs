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
using TeamUp.ModelViews.CoachBookingModelViews;
using TeamUp.ModelViews.CourtModelViews;
using TeamUp.ModelViews.UserModelViews.Response;
using TeamUp.Repositories.Entity;
using Microsoft.EntityFrameworkCore;
using static BabyCare.Core.Utils.SystemConstant;
using TeamUp.ModelViews.SportsComplexModelViews;
using TeamUp.Core.Utils;
using System.Globalization;
using Firebase.Auth;

namespace TeamUp.Services.Service
{
    public class CoachBookingService : ICoachBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public CoachBookingService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<ApiResult<BasePaginatedList<CoachBookingModelView>>> GetAllCoachBookingAsync(int pageNumber, int pageSize, int? coachId, int? userId, int? courtId, TimeSpan? startTime, TimeSpan? endTime, string? status)
        {
            var query = _unitOfWork.GetRepository<CoachBooking>().Entities
                .Include(cb => cb.Coach)
                .Include(cb => cb.Player)
                .Include(cb => cb.Court)
                .Where(cb => !cb.DeletedTime.HasValue);

            if (coachId.HasValue) query = query.Where(cb => cb.CoachId == coachId.Value);
            if (userId.HasValue) query = query.Where(cb => cb.PlayerId == userId.Value);
            if (courtId.HasValue) query = query.Where(cb => cb.CourtId == courtId.Value);
            if (startTime.HasValue) query = query.Where(cb => cb.StartTime >= startTime.Value);
            if (endTime.HasValue) query = query.Where(cb => cb.EndTime <= endTime.Value);
            if (!string.IsNullOrWhiteSpace(status)) query = query.Where(cb => cb.Status == status);

            int totalCount = await query.CountAsync();
            var bookings = await query.OrderByDescending(cb => cb.CreatedTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = _mapper.Map<List<CoachBookingModelView>>(bookings);

            for (int i = 0; i < result.Count; i++)
            {
                result[i].Player = _mapper.Map<UserResponseModel>(bookings[i].Player);
                result[i].Court = _mapper.Map<CourtModelView>(bookings[i].Court);
                result[i].Coach = _mapper.Map<EmployeeResponseModel>(bookings[i].Coach);
                result[i].Court.SportsComplexModelView = _mapper.Map<SportsComplexModelView>(bookings[i].Court.SportsComplex);
            }

            return new ApiSuccessResult<BasePaginatedList<CoachBookingModelView>>(new BasePaginatedList<CoachBookingModelView>(result, totalCount, pageNumber, pageSize));
        }

        public async Task<ApiResult<List<CoachBookingModelView>>> GetAllCoachBooking()
        {
            var bookings = await _unitOfWork.GetRepository<CoachBooking>().Entities
                .Include(cb => cb.Coach)
                .Include(cb => cb.Player)
                .Include(cb => cb.Court)
                .OrderByDescending(cb => cb.CreatedTime)
                .Where(cb => !cb.DeletedTime.HasValue)
                .ToListAsync();

            var result = _mapper.Map<List<CoachBookingModelView>>(bookings);

            for (int i = 0; i < result.Count; i++)
            {
                result[i].Player = _mapper.Map<UserResponseModel>(bookings[i].Player);
                result[i].Court = _mapper.Map<CourtModelView>(bookings[i].Court);
                result[i].Coach = _mapper.Map<EmployeeResponseModel>(bookings[i].Coach);
                result[i].Court.SportsComplexModelView = _mapper.Map<SportsComplexModelView>(bookings[i].Court.SportsComplex);
            }

            return new ApiSuccessResult<List<CoachBookingModelView>>(result);
        }

        public async Task<ApiResult<CoachBookingModelView>> GetCoachBookingByIdAsync(int id)
        {
            var booking = await _unitOfWork.GetRepository<CoachBooking>().Entities
                .Include(cb => cb.Coach)
                .Include(cb => cb.Player)
                .Include(cb => cb.Court)
                .FirstOrDefaultAsync(cb => cb.Id == id && !cb.DeletedTime.HasValue);

            if (booking == null) return new ApiErrorResult<CoachBookingModelView>("Không tìm thấy lịch huấn luyện.");

            var result = _mapper.Map<CoachBookingModelView>(booking);
            result.Player = _mapper.Map<UserResponseModel>(booking.Player);
            result.Court = _mapper.Map<CourtModelView>(booking.Court);
            result.Coach = _mapper.Map<EmployeeResponseModel>(booking.Coach);
            result.Court.SportsComplexModelView = _mapper.Map<SportsComplexModelView>(booking.Court.SportsComplex);

            return new ApiSuccessResult<CoachBookingModelView>(result);
        }

        public async Task<ApiResult<object>> AddCoachBookingAsync(CreateCoachBookingModelView model)
        {
            var coach = await _unitOfWork.GetRepository<ApplicationUser>().GetByIdAsync(model.CoachId);
            var user = await _unitOfWork.GetRepository<ApplicationUser>().GetByIdAsync(model.PlayerId);
            var court = await _unitOfWork.GetRepository<Court>().GetByIdAsync(model.CourtId);

            if (coach == null || user == null || court == null)
                return new ApiErrorResult<object>("Dữ liệu không hợp lệ.");

            var distinctDates = model.SelectedDates.Select(d => d.Date).Distinct().ToList();

            // ❗ Kiểm tra trùng lịch HLV
            bool isCoachConflict = await _unitOfWork.GetRepository<CoachBooking>().Entities
                .AnyAsync(b =>
                    b.CoachId == model.CoachId &&
                    !b.DeletedTime.HasValue &&
                    b.SelectedDates.Any(d => distinctDates.Contains(d.Date)) &&
                    (
                        (model.StartTime >= b.StartTime && model.StartTime < b.EndTime) ||
                        (model.EndTime > b.StartTime && model.EndTime <= b.EndTime) ||
                        (model.StartTime <= b.StartTime && model.EndTime >= b.EndTime)
                    )
                );

            if (isCoachConflict)
                return new ApiErrorResult<object>("Huấn luyện viên đã có lịch vào khung giờ này.");

            // ❗ Kiểm tra trùng lịch sân theo ngày + giờ
            foreach (var date in distinctDates)
            {
                var courtStart = date.Date + model.StartTime;
                var courtEnd = date.Date + model.EndTime;

                bool isCourtConflict = await _unitOfWork.GetRepository<CourtBooking>().Entities
                    .AnyAsync(b =>
                        b.CourtId == model.CourtId &&
                        !b.DeletedTime.HasValue &&
                        (
                            (courtStart >= b.StartTime && courtStart < b.EndTime) ||
                            (courtEnd > b.StartTime && courtEnd <= b.EndTime) ||
                            (courtStart <= b.StartTime && courtEnd >= b.EndTime)
                        )
                    );

                if (isCourtConflict)
                    return new ApiErrorResult<object>($"Sân đã được đặt vào khung giờ {model.StartTime:hh\\:mm} - {model.EndTime:hh\\:mm} ngày {date:dd/MM/yyyy}.");
            }

            var booking = _mapper.Map<CoachBooking>(model);
            booking.CreatedTime = DateTime.Now;
            booking.CreatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            // ❗ Tính tổng tiền
            decimal durationHours = (decimal)(model.EndTime - model.StartTime).TotalHours;
            decimal courtFee = durationHours * court.PricePerHour * distinctDates.Count;
            decimal coachFee = distinctDates.Count * (coach.PricePerSession ?? 0);
            booking.TotalPrice = courtFee + coachFee;

            booking.Status = BookingStatus.Pending;
            booking.PaymentStatus = PaymentStatus.Pending;

            await _unitOfWork.GetRepository<CoachBooking>().InsertAsync(booking);
            await _unitOfWork.SaveAsync();

            // ✅ Gửi email xác nhận sử dụng DoingMail
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormSendEmail", "CoachBookingSuccess.html");
            path = Path.GetFullPath(path);

            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);

                content = content.Replace("{{UserEmail}}", user.Email);
                content = content.Replace("{{CoachEmail}}", coach.Email);
                content = content.Replace("{{CourtName}}", court.Name);
                content = content.Replace("{{Date}}", string.Join(", ", distinctDates.Select(d => d.ToString("dd/MM/yyyy"))));
                content = content.Replace("{{StartTime}}", model.StartTime.ToString(@"hh\:mm"));
                content = content.Replace("{{EndTime}}", model.EndTime.ToString(@"hh\:mm"));
                content = content.Replace("{{TotalPrice}}", booking.TotalPrice.ToString("N0", new CultureInfo("vi-VN")));
                content = content.Replace("{{Status}}", booking.Status.ToString());

                DoingMail.SendMail("TeamUp", "Xác nhận đặt huấn luyện viên", content, user.Email);
            }

            return new ApiSuccessResult<object>("Đặt huấn luyện viên thành công và đã gửi xác nhận email.");
        }


        public async Task<ApiResult<object>> UpdateCoachBookingAsync(int id, UpdateCoachBookingModelView model)
        {
            var repo = _unitOfWork.GetRepository<CoachBooking>();
            var booking = await repo.Entities.FirstOrDefaultAsync(b => b.Id == id && !b.DeletedTime.HasValue);
            if (booking == null)
                return new ApiErrorResult<object>("Không tìm thấy lịch huấn luyện.");

            var updatedStart = model.StartTime ?? booking.StartTime;
            var updatedEnd = model.EndTime ?? booking.EndTime;
            var updatedCourtId = model.CourtId ?? booking.CourtId;

            // ❗ Kiểm tra trùng lịch HLV
            bool isCoachConflict = await _unitOfWork.GetRepository<CoachBooking>().Entities
                .AnyAsync(b =>
                    b.Id != id &&
                    b.CoachId == booking.CoachId &&
                    !b.DeletedTime.HasValue &&
                    b.SelectedDates.Any(d => booking.SelectedDates.Contains(d.Date)) &&
                    (
                        (updatedStart >= b.StartTime && updatedStart < b.EndTime) ||
                        (updatedEnd > b.StartTime && updatedEnd <= b.EndTime) ||
                        (updatedStart <= b.StartTime && updatedEnd >= b.EndTime)
                    )
                );

            if (isCoachConflict)
                return new ApiErrorResult<object>("Lịch huấn luyện viên bị trùng.");

            // ❗ Kiểm tra trùng lịch sân
            foreach (var date in booking.SelectedDates)
            {
                DateTime dateStart = date.Date + updatedStart;
                DateTime dateEnd = date.Date + updatedEnd;

                bool isCourtConflict = await _unitOfWork.GetRepository<CourtBooking>().Entities
                    .AnyAsync(b =>
                        b.Id != id &&
                        b.CourtId == updatedCourtId &&
                        !b.DeletedTime.HasValue &&
                        (
                            (dateStart >= b.StartTime && dateStart < b.EndTime) ||
                            (dateEnd > b.StartTime && dateEnd <= b.EndTime) ||
                            (dateStart <= b.StartTime && dateEnd >= b.EndTime)
                        )
                    );

                if (isCourtConflict)
                    return new ApiErrorResult<object>($"Sân đã được đặt vào khung giờ {updatedStart:hh\\:mm} - {updatedEnd:hh\\:mm} ngày {date:dd/MM/yyyy}.");
            }

            // ✅ Cập nhật dữ liệu
            booking.StartTime = updatedStart;
            booking.EndTime = updatedEnd;
            booking.CourtId = updatedCourtId;

            if (model.PlayerId.HasValue)
                booking.PlayerId = model.PlayerId.Value;

            if (model.CoachId.HasValue)
                booking.CoachId = model.CoachId.Value;

            if (!string.IsNullOrEmpty(model.Status))
            {
                var validStatuses = new[]
                {
                    BookingStatus.Pending, BookingStatus.Confirmed, BookingStatus.InProgress,
                    BookingStatus.Completed, BookingStatus.CancelledByUser, BookingStatus.CancelledByOwner,
                    BookingStatus.NoShow, BookingStatus.Failed, BookingStatus.CancelledByCoach
                };

                if (!validStatuses.Contains(model.Status))
                    return new ApiErrorResult<object>("Trạng thái không hợp lệ.");

                booking.Status = model.Status;
            }

            if (!string.IsNullOrEmpty(model.PaymentMethod))
                booking.PaymentMethod = model.PaymentMethod;

            booking.LastUpdatedTime = DateTime.Now;
            booking.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            // ✅ Tính lại TotalPrice
            var court = await _unitOfWork.GetRepository<Court>().GetByIdAsync(booking.CourtId);
            var coach = await _unitOfWork.GetRepository<ApplicationUser>().GetByIdAsync(booking.CoachId);

            if (court != null && coach != null)
            {
                decimal courtFee = (decimal)(booking.EndTime - booking.StartTime).TotalHours * court.PricePerHour * booking.SelectedDates.Count;
                decimal coachFee = booking.SelectedDates.Count * (coach.PricePerSession ?? 0);
                booking.TotalPrice = courtFee + coachFee;

            }

            await repo.UpdateAsync(booking);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Cập nhật thành công.");
        }



        public async Task<ApiResult<object>> DeleteCoachBookingAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<CoachBooking>();
            var booking = await repo.Entities.FirstOrDefaultAsync(b => b.Id == id && !b.DeletedTime.HasValue);
            if (booking == null)
                return new ApiErrorResult<object>("Không tìm thấy lịch huấn luyện.");

            booking.DeletedTime = DateTime.Now;
            booking.DeletedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");
            booking.Status = BookingStatus.Failed;

            await repo.UpdateAsync(booking);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Xóa thành công.");
        }

        public async Task<ApiResult<object>> UpdateCoachBookingStatusAsync(int id, string? status, string? paymentStatus)
        {
            var booking = await _unitOfWork.GetRepository<CoachBooking>().Entities
                .FirstOrDefaultAsync(b => b.Id == id && !b.DeletedTime.HasValue);
            if (booking == null)
                return new ApiErrorResult<object>("Không tìm thấy lịch huấn luyện.");

            if (!string.IsNullOrWhiteSpace(status))
            {
                var validStatuses = new[]
                {
                    BookingStatus.Pending, BookingStatus.Confirmed, BookingStatus.InProgress,
                    BookingStatus.Completed, BookingStatus.CancelledByUser, BookingStatus.CancelledByOwner,
                    BookingStatus.NoShow, BookingStatus.Failed, BookingStatus.CancelledByCoach
                };

                if (!validStatuses.Contains(status))
                    return new ApiErrorResult<object>("Trạng thái không hợp lệ.");

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

            booking.LastUpdatedTime = DateTime.Now;
            booking.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            await _unitOfWork.GetRepository<CoachBooking>().UpdateAsync(booking);
            await _unitOfWork.SaveAsync();

            // 📨 Gửi email xác nhận nếu trạng thái là Confirmed
            if (booking.Status == BookingStatus.Confirmed)
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormSendEmail", "CoachBookingConfirmed.html");
                path = Path.GetFullPath(path);

                if (File.Exists(path))
                {
                    var content = File.ReadAllText(path);

                    content = content.Replace("{{UserEmail}}", booking.Player?.Email);
                    content = content.Replace("{{CoachEmail}}", booking.Coach?.Email);
                    content = content.Replace("{{CourtName}}", booking.Court?.Name);
                    content = content.Replace("{{Date}}", string.Join(", ", booking.SelectedDates.Select(d => d.ToString("dd/MM/yyyy"))));
                    content = content.Replace("{{StartTime}}", booking.StartTime.ToString(@"hh\:mm"));
                    content = content.Replace("{{EndTime}}", booking.EndTime.ToString(@"hh\:mm"));
                    content = content.Replace("{{TotalPrice}}", booking.TotalPrice.ToString("N0", new CultureInfo("vi-VN")));
                    content = content.Replace("{{Status}}", booking.Status.ToString());
                    content = content.Replace("{{PaymentStatus}}", booking.PaymentStatus);

                    DoingMail.SendMail("TeamUp", "Xác nhận đặt huấn luyện viên", content, booking.Player?.Email);
                }
            }

            return new ApiSuccessResult<object>("Cập nhật trạng thái thành công.");
        }

        public async Task<ApiResult<object>> GetTotalPriceInMonthForCoachAndAdmin(int coachId, string paymentMethod, int month, int year)
        {
            var bookings = await _unitOfWork.GetRepository<CoachBooking>().Entities
                .Include(b => b.Coach)
                .Where(b =>
                    b.CoachId == coachId &&
                    !b.DeletedTime.HasValue &&
                    b.Status == BookingStatus.Completed &&
                    b.PaymentMethod == paymentMethod &&
                    b.SelectedDates.Any(date => date.Month == month && date.Year == year))
                .ToListAsync();

            var totalPrice = bookings.Sum(b => b.TotalPrice);

            return new ApiSuccessResult<object>(new
            {
                TotalPrice = totalPrice
            });
        }




        public async Task<ApiResult<object>> GetCoachBookingStatsAsync(int coachId)
        {
            var query = _unitOfWork.GetRepository<CoachBooking>().Entities
                .Where(cb => cb.CoachId == coachId && !cb.DeletedTime.HasValue);

            var totalBookings = await query.CountAsync();

            var totalRevenue = await query
                .Where(cb => cb.PaymentStatus == "Paid")
                .SumAsync(cb => cb.TotalPrice);

            var uniqueStudents = await query
                .Select(cb => cb.PlayerId)
                .Distinct()
                .CountAsync();

            var result = new
            {
                CoachId = coachId,
                TotalBookings = totalBookings,
                TotalRevenue = totalRevenue,
                UniqueStudents = uniqueStudents
            };

            return new ApiSuccessResult<object>(result);
        }

        public async Task<ApiResult<List<object>>> GetPlayersByCoachAsync(int coachId)
        {
            var bookings = await _unitOfWork.GetRepository<CoachBooking>().Entities
                .Where(cb => cb.CoachId == coachId && !cb.DeletedTime.HasValue)
                .Include(cb => cb.Player)
                .GroupBy(cb => cb.PlayerId)
                .Select(g => new
                {
                    PlayerId = g.Key,
                    PlayerInfo = g.First().Player
                })
                .ToListAsync();

            var result = bookings.Select(b => new
            {
                b.PlayerId,
                FullName = b.PlayerInfo.FullName,
                Email = b.PlayerInfo.Email,
                PhoneNumber = b.PlayerInfo.PhoneNumber,
                AvaterUrl = b.PlayerInfo.AvatarUrl
            }).ToList<object>();

            return new ApiSuccessResult<List<object>>(result);
        }


        public async Task<ApiResult<object>> GetTotalBookingsThisMonthByCoachAsync(int coachId)
        {
            var now = DateTime.UtcNow;
            var startMonth = new DateTime(now.Year, now.Month, 1);
            var endMonth = startMonth.AddMonths(1);

            var bookings = await _unitOfWork.GetRepository<CoachBooking>().Entities
                .Where(cb => cb.CoachId == coachId && !cb.DeletedTime.HasValue &&
                             cb.CreatedTime >= startMonth && cb.CreatedTime < endMonth)
                .CountAsync();

            return new ApiSuccessResult<object>(new
            {
                CoachId = coachId,
                Month = now.Month,
                Year = now.Year,
                TotalBookings = bookings
            });
        }

        public async Task<ApiResult<List<object>>> GetBookedSlotsThisWeekByCoachAsync(int coachId)
        {
            DateTime now = DateTime.Now;
            int diff = (7 + (now.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime startOfWeek = now.Date.AddDays(-1 * diff);
            DateTime endOfWeek = startOfWeek.AddDays(7).AddSeconds(-1);

            var bookings = await _unitOfWork.GetRepository<CoachBooking>().Entities
                .Where(cb => cb.CoachId == coachId &&
                             !cb.DeletedTime.HasValue &&
                             cb.SelectedDates.Any(d => d >= startOfWeek && d < endOfWeek)
                             && (
                                cb.Status == BookingStatus.Pending ||
                                cb.Status == BookingStatus.Confirmed ||
                                cb.Status == BookingStatus.InProgress ||
                                cb.Status == BookingStatus.Completed
                             ))
                .ToListAsync();

            var result = new List<object>();

            foreach (var booking in bookings)
            {
                foreach (var date in booking.SelectedDates.Where(d => d >= startOfWeek && d < endOfWeek))
                {
                    result.Add(new
                    {
                        booking.CourtId,
                        WeekDay = date.ToString("dddd", new CultureInfo("vi-VN")),
                        Date = date.ToString("yyyy-MM-dd"),
                        BookedSlot = $"{booking.StartTime:hh\\:mm} - {booking.EndTime:hh\\:mm}"
                    });
                }
            }

            return new ApiSuccessResult<List<object>>(result);
        }
    }
}
