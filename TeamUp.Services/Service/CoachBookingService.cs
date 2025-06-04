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
using TeamUp.ModelViews.VoucherModelViews;

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

        public async Task<ApiResult<BasePaginatedList<CoachBookingModelView>>> GetAllCoachBookingAsync(
            int pageNumber, int pageSize, int? coachId, int? userId, int? courtId,
            DateTime? startTime, DateTime? endTime, string? status)
        {
            var query = _unitOfWork.GetRepository<CoachBooking>().Entities
                .Include(cb => cb.Coach)
                .Include(cb => cb.Player)
                .Include(cb => cb.Court).ThenInclude(c => c.SportsComplex)
                .Include(cb => cb.Voucher)
                .Include(cb => cb.Slots)
                .Where(cb => !cb.DeletedTime.HasValue);

            if (coachId.HasValue)
                query = query.Where(cb => cb.CoachId == coachId.Value);

            if (userId.HasValue)
                query = query.Where(cb => cb.PlayerId == userId.Value);

            if (courtId.HasValue)
                query = query.Where(cb => cb.CourtId == courtId.Value);

            if (startTime.HasValue)
                query = query.Where(cb => cb.Slots.Any(s => s.StartTime >= startTime.Value));

            if (endTime.HasValue)
                query = query.Where(cb => cb.Slots.Any(s => s.EndTime <= endTime.Value));

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(cb => cb.Status == status);

            int totalCount = await query.CountAsync();

            var bookings = await query.OrderByDescending(cb => cb.CreatedTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = _mapper.Map<List<CoachBookingModelView>>(bookings);

            for (int i = 0; i < result.Count; i++)
            {
                var booking = bookings[i];
                result[i].Player = _mapper.Map<UserResponseModel>(booking.Player);
                result[i].Coach = _mapper.Map<EmployeeResponseModel>(booking.Coach);
                result[i].Court = _mapper.Map<CourtModelView>(booking.Court);
                result[i].Court.SportsComplexModelView = _mapper.Map<SportsComplexModelView>(booking.Court.SportsComplex);
                result[i].Voucher = booking.Voucher != null ? _mapper.Map<VoucherModelView>(booking.Voucher) : null;

                result[i].Slots = booking.Slots
                    .Select(slot => new SlotModelView
                    {
                        Id = slot.Id,
                        CoachId = booking.CoachId,
                        StartTime = slot.StartTime,
                        EndTime = slot.EndTime
                    })
                    .ToList();
            }

            return new ApiSuccessResult<BasePaginatedList<CoachBookingModelView>>(
                new BasePaginatedList<CoachBookingModelView>(result, totalCount, pageNumber, pageSize));
        }


        public async Task<ApiResult<List<CoachBookingModelView>>> GetAllCoachBooking()
        {
            var bookings = await _unitOfWork.GetRepository<CoachBooking>().Entities
                .Include(cb => cb.Coach)
                .Include(cb => cb.Player)
                .Include(cb => cb.Court).ThenInclude(c => c.SportsComplex)
                .Include(cb => cb.Voucher)
                .Include(cb => cb.Slots)
                .Where(cb => !cb.DeletedTime.HasValue)
                .OrderByDescending(cb => cb.CreatedTime)
                .ToListAsync();

            var result = _mapper.Map<List<CoachBookingModelView>>(bookings);

            for (int i = 0; i < result.Count; i++)
            {
                var booking = bookings[i];

                result[i].Player = _mapper.Map<UserResponseModel>(booking.Player);
                result[i].Coach = _mapper.Map<EmployeeResponseModel>(booking.Coach);
                result[i].Court = _mapper.Map<CourtModelView>(booking.Court);
                result[i].Court.SportsComplexModelView = _mapper.Map<SportsComplexModelView>(booking.Court.SportsComplex);
                result[i].Voucher = booking.Voucher != null ? _mapper.Map<VoucherModelView>(booking.Voucher) : null;

                result[i].Slots = booking.Slots
                    .Select(slot => new SlotModelView
                    {
                        Id = slot.Id,
                        CoachId = booking.CoachId,
                        StartTime = slot.StartTime,
                        EndTime = slot.EndTime
                    })
                    .ToList();
            }

            return new ApiSuccessResult<List<CoachBookingModelView>>(result);
        }


        public async Task<ApiResult<CoachBookingModelView>> GetCoachBookingByIdAsync(int id)
        {
            var booking = await _unitOfWork.GetRepository<CoachBooking>().Entities
                .Include(cb => cb.Coach)
                .Include(cb => cb.Player)
                .Include(cb => cb.Court).ThenInclude(c => c.SportsComplex)
                .Include(cb => cb.Voucher)
                .Include(cb => cb.Slots)
                .FirstOrDefaultAsync(cb => cb.Id == id && !cb.DeletedTime.HasValue);

            if (booking == null)
                return new ApiErrorResult<CoachBookingModelView>("Không tìm thấy lịch huấn luyện.");

            var result = _mapper.Map<CoachBookingModelView>(booking);
            result.Player = _mapper.Map<UserResponseModel>(booking.Player);
            result.Coach = _mapper.Map<EmployeeResponseModel>(booking.Coach);
            result.Court = _mapper.Map<CourtModelView>(booking.Court);
            result.Court.SportsComplexModelView = _mapper.Map<SportsComplexModelView>(booking.Court.SportsComplex);
            result.Voucher = booking.Voucher != null ? _mapper.Map<VoucherModelView>(booking.Voucher) : null;

            result.Slots = booking.Slots
                .Select(slot => new SlotModelView
                {
                    Id = slot.Id,
                    CoachId = booking.CoachId,
                    StartTime = slot.StartTime,
                    EndTime = slot.EndTime
                })
                .ToList();

            return new ApiSuccessResult<CoachBookingModelView>(result);
        }


        public async Task<ApiResult<object>> AddCoachBookingAsync(CreateCoachBookingModelView model)
        {
            var coach = await _unitOfWork.GetRepository<ApplicationUser>().GetByIdAsync(model.CoachId);
            var user = await _unitOfWork.GetRepository<ApplicationUser>().GetByIdAsync(model.PlayerId);
            var court = await _unitOfWork.GetRepository<Court>().GetByIdAsync(model.CourtId);

            if (coach == null || user == null || court == null)
                return new ApiErrorResult<object>("Dữ liệu không hợp lệ.");


            // Kiểm tra trùng lịch huấn luyện viên
            foreach (var slot in model.Slots)
            {
                bool isCoachConflict = await _unitOfWork.GetRepository<Slot>().Entities
                    .AnyAsync(s => 
                        s.CoachBooking.CoachId == model.CoachId && s.CoachBooking.PaymentStatus == "Paid" &&
                        !s.CoachBooking.DeletedTime.HasValue &&
                        (
                            (slot.StartTime >= s.StartTime && slot.StartTime < s.EndTime) ||
                            (slot.EndTime > s.StartTime && slot.EndTime <= s.EndTime) ||
                            (slot.StartTime <= s.StartTime && slot.EndTime >= s.EndTime)
                        )
                    );

                if (isCoachConflict)
                    return new ApiErrorResult<object>($"Huấn luyện viên đã có lịch vào khung giờ {slot.StartTime:HH:mm} - {slot.EndTime:HH:mm} ngày {slot.StartTime:dd/MM/yyyy}.");
            }

            // Kiểm tra trùng lịch sân
            foreach (var slot in model.Slots)
            {
                bool isCourtConflict = await _unitOfWork.GetRepository<CourtBooking>().Entities
                    .AnyAsync(b =>
                        b.CourtId == model.CourtId && b.PaymentStatus == "Paid" &&
                        !b.DeletedTime.HasValue &&
                        (
                            (slot.StartTime >= b.StartTime && slot.StartTime < b.EndTime) ||
                            (slot.EndTime > b.StartTime && slot.EndTime <= b.EndTime) ||
                            (slot.StartTime <= b.StartTime && slot.EndTime >= b.EndTime)
                        )
                    );

                if (isCourtConflict)
                    return new ApiErrorResult<object>($"Sân đã được đặt vào khung giờ {slot.StartTime:HH:mm} - {slot.EndTime:HH:mm} ngày {slot.StartTime:dd/MM/yyyy}.");
            }

            // Tạo booking
            var booking = new CoachBooking
            {
                CoachId = model.CoachId,
                PlayerId = model.PlayerId,
                CourtId = model.CourtId,
                PaymentMethod = model.PaymentMethod,
                Status = BookingStatus.Pending,
                PaymentStatus = PaymentStatus.Pending,
                CreatedTime = DateTime.Now,
                CreatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0"),
                Slots = new List<Slot>()
            };

            // Tính tổng tiền
            decimal totalHours = 0;
            foreach (var slot in model.Slots)
            {
                totalHours += (decimal)(slot.EndTime - slot.StartTime).TotalHours;

                var newSlot = new Slot
                {
                    CoachBookingId = booking.Id,
                    StartTime = slot.StartTime,
                    EndTime = slot.EndTime
                };

                booking.Slots.Add(newSlot);

                await _unitOfWork.GetRepository<Slot>().InsertAsync(newSlot);

            }

            decimal courtFee = totalHours * court.PricePerHour;
            decimal coachFee = model.Slots.Count * (coach.PricePerSession ?? 0);
            booking.TotalPrice = courtFee + coachFee;

            // Áp dụng Voucher
            if (model.VoucherId != null)
            {
                var voucher = await _unitOfWork.GetRepository<Voucher>().Entities
                    .FirstOrDefaultAsync(v => v.Id == model.VoucherId);

                if (voucher != null)
                {
                    if (voucher.Code == "VOUCHER1")
                    {
                        bool isFirstBooking = !await _unitOfWork.GetRepository<CoachBooking>().Entities
                            .AnyAsync(b => b.PlayerId == model.PlayerId && !b.DeletedTime.HasValue && b.VoucherId == 1);

                        if (isFirstBooking)
                        {
                            booking.DiscountAmount = booking.TotalPrice * voucher.DiscountPercent / 100;
                            booking.TotalPrice -= booking.DiscountAmount;
                            booking.VoucherId = voucher.Id;
                        }
                        else
                        {
                            return new ApiErrorResult<object>("VOUCHER1 chỉ được sử dụng với lần đầu đặt huấn luyện viên.");
                        }
                    }
                    else
                    {
                        booking.DiscountAmount = booking.TotalPrice * voucher.DiscountPercent / 100;
                        booking.TotalPrice -= booking.DiscountAmount;
                        booking.VoucherId = voucher.Id;
                    }
                }
            }

            await _unitOfWork.GetRepository<CoachBooking>().InsertAsync(booking);
            await _unitOfWork.SaveAsync();

            // Gửi email xác nhận
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormSendEmail", "CoachBookingSuccess.html");
            path = Path.GetFullPath(path);

            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);

                content = content.Replace("{{UserEmail}}", user.Email);
                content = content.Replace("{{CoachEmail}}", coach.Email);
                content = content.Replace("{{CourtName}}", court.Name);
                var slotRows = new StringBuilder();
                for (int i = 0; i < model.Slots.Count; i++)
                {
                    var slot = model.Slots[i];
                    slotRows.AppendLine($@"
                    <tr>
                        <td style='padding: 6px 12px; text-align: center;'>{i + 1}</td>
                        <td style='padding: 6px 12px;'>{slot.StartTime:dd/MM/yyyy}</td>
                        <td style='padding: 6px 12px;'>{slot.StartTime:HH:mm}</td>
                        <td style='padding: 6px 12px;'>{slot.EndTime:HH:mm}</td>
                    </tr>");
                }
                content = content.Replace("{{SlotItems}}", slotRows.ToString());
                content = content.Replace("{{TotalPrice}}", booking.TotalPrice.ToString("N0", new CultureInfo("vi-VN")));
                content = content.Replace("{{Status}}", booking.Status);
                content = content.Replace("{{PaymentStatus}}", booking.PaymentStatus);

                DoingMail.SendMail("TeamUp", "Xác nhận đặt huấn luyện viên", content, user.Email);
            }

            return new ApiSuccessResult<object>("Đặt huấn luyện viên thành công và đã gửi xác nhận email.");
        }



        public async Task<ApiResult<object>> UpdateCoachBookingAsync(int id, UpdateCoachBookingModelView model)
        {
            var repo = _unitOfWork.GetRepository<CoachBooking>();
            var booking = await repo.Entities
                .Include(b => b.Slots)
                .FirstOrDefaultAsync(b => b.Id == id && !b.DeletedTime.HasValue);

            if (booking == null)
                return new ApiErrorResult<object>("Không tìm thấy lịch huấn luyện.");

            var updatedCourtId = model.CourtId ?? booking.CourtId;
            var updatedCoachId = model.CoachId ?? booking.CoachId;
            var updatedPlayerId = model.PlayerId ?? booking.PlayerId;

            // ❗ Kiểm tra trùng lịch HLV và sân theo từng Slot
            if (model.Slots != null)
            {
                foreach (var slot in model.Slots)
                {
                    // Trùng lịch huấn luyện viên
                    bool isCoachConflict = await _unitOfWork.GetRepository<Slot>().Entities
                        .Include(s => s.CoachBooking)
                        .AnyAsync(s =>
                            s.CoachBookingId != id &&
                            s.CoachBooking.CoachId == updatedCoachId &&
                            !s.CoachBooking.DeletedTime.HasValue &&
                            (
                                (slot.StartTime >= s.StartTime && slot.StartTime < s.EndTime) ||
                                (slot.EndTime > s.StartTime && slot.EndTime <= s.EndTime) ||
                                (slot.StartTime <= s.StartTime && slot.EndTime >= s.EndTime)
                            )
                        );

                    if (isCoachConflict)
                        return new ApiErrorResult<object>($"Lịch huấn luyện viên bị trùng vào {slot.StartTime:dd/MM/yyyy HH:mm} - {slot.EndTime:HH:mm}");

                    // Trùng lịch sân
                    bool isCourtConflict = await _unitOfWork.GetRepository<CourtBooking>().Entities
                        .AnyAsync(cb =>
                            cb.CourtId == updatedCourtId &&
                            !cb.DeletedTime.HasValue &&
                            (
                                (slot.StartTime >= cb.StartTime && slot.StartTime < cb.EndTime) ||
                                (slot.EndTime > cb.StartTime && slot.EndTime <= cb.EndTime) ||
                                (slot.StartTime <= cb.StartTime && slot.EndTime >= cb.EndTime)
                            )
                        );

                    if (isCourtConflict)
                        return new ApiErrorResult<object>($"Sân đã được đặt vào {slot.StartTime:dd/MM/yyyy HH:mm} - {slot.EndTime:HH:mm}");
                }

                // Xóa slot cũ
                var slotRepo = _unitOfWork.GetRepository<Slot>();
                var oldSlots = await slotRepo.Entities.Where(s => s.CoachBookingId == id).ToListAsync();
                foreach (var s in oldSlots)
                    await slotRepo.DeleteAsync(s);

                // Thêm slot mới
                foreach (var s in model.Slots)
                {
                    await slotRepo.InsertAsync(new Slot
                    {
                        CoachBookingId = booking.Id,
                        StartTime = s.StartTime,
                        EndTime = s.EndTime
                    });
                }
            }

            // ✅ Cập nhật thông tin
            booking.CoachId = updatedCoachId;
            booking.PlayerId = updatedPlayerId;
            booking.CourtId = updatedCourtId;

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

            // ✅ Tính lại tổng giá
            var court = await _unitOfWork.GetRepository<Court>().GetByIdAsync(booking.CourtId);
            var coach = await _unitOfWork.GetRepository<ApplicationUser>().GetByIdAsync(booking.CoachId);
            var allNewSlots = await _unitOfWork.GetRepository<Slot>().Entities
                .Where(s => s.CoachBookingId == booking.Id).ToListAsync();

            decimal totalCourtFee = 0;
            decimal totalCoachFee = 0;

            foreach (var slot in allNewSlots)
            {
                var durationHours = (decimal)(slot.EndTime - slot.StartTime).TotalHours;
                totalCourtFee += durationHours * court.PricePerHour;
                totalCoachFee += coach.PricePerSession ?? 0;
            }

            booking.TotalPrice = totalCourtFee + totalCoachFee;

            // ✅ Áp dụng voucher nếu có
            if (model.VoucherId != null)
            {
                var voucher = await _unitOfWork.GetRepository<Voucher>().GetByIdAsync(model.VoucherId.Value);

                if (voucher != null)
                {
                    if (voucher.Code == "VOUCHER1")
                    {
                        bool isFirstBooking = !await _unitOfWork.GetRepository<CoachBooking>().Entities
                            .AnyAsync(b => b.PlayerId == model.PlayerId && !b.DeletedTime.HasValue && b.VoucherId.HasValue);

                        if (isFirstBooking)
                        {
                            booking.DiscountAmount = booking.TotalPrice * voucher.DiscountPercent / 100;
                            booking.TotalPrice -= booking.DiscountAmount;
                            booking.VoucherId = voucher.Id;
                        }
                        else
                        {
                            return new ApiErrorResult<object>("VOUCHER1 chỉ được sử dụng với lần đầu đặt sân.");
                        }
                    }
                    else
                    {
                        booking.DiscountAmount = booking.TotalPrice * voucher.DiscountPercent / 100;
                        booking.TotalPrice -= booking.DiscountAmount;
                        booking.VoucherId = voucher.Id;
                    }
                }
            }

            booking.LastUpdatedTime = DateTime.Now;
            booking.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

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
                    // ✅ Thêm danh sách slot
                    var slotRows = new StringBuilder();
                    var slots = booking.Slots?.OrderBy(s => s.StartTime).ToList(); // đảm bảo có danh sách Slot
                    if (slots != null)
                    {
                        for (int i = 0; i < slots.Count; i++)
                        {
                            var slot = slots[i];
                            slotRows.AppendLine($@"
                            <tr>
                                <td style='padding: 6px 12px; text-align: center;'>{i + 1}</td>
                                <td style='padding: 6px 12px;'>{slot.StartTime:dd/MM/yyyy}</td>
                                <td style='padding: 6px 12px;'>{slot.StartTime:HH:mm}</td>
                                <td style='padding: 6px 12px;'>{slot.EndTime:HH:mm}</td>
                            </tr>");
                        }
                    }

                    content = content.Replace("{{SlotItems}}", slotRows.ToString());

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
                .Include(b => b.Slots)  // Load Slots
                .Where(b =>
                    b.CoachId == coachId &&
                    !b.DeletedTime.HasValue &&
                    b.Status == BookingStatus.Completed &&
                    b.PaymentMethod == paymentMethod &&
                    b.Slots.Any(slot => slot.StartTime.Month == month && slot.StartTime.Year == year)
                )
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

            var validStatuses = new[] { BookingStatus.Pending, BookingStatus.Confirmed, BookingStatus.InProgress, BookingStatus.Completed };

            var bookings = await _unitOfWork.GetRepository<CoachBooking>().Entities
                .Include(cb => cb.Slots)
                .Where(cb => cb.CoachId == coachId &&
                             !cb.DeletedTime.HasValue &&
                             validStatuses.Contains(cb.Status) &&
                             cb.Slots.Any(slot => slot.StartTime >= startOfWeek && slot.EndTime <= endOfWeek))
                .ToListAsync();

            var result = new List<object>();

            foreach (var booking in bookings)
            {
                foreach (var slot in booking.Slots.Where(slot => slot.StartTime >= startOfWeek && slot.EndTime <= endOfWeek))
                {
                    result.Add(new
                    {
                        booking.CourtId,
                        WeekDay = slot.StartTime.ToString("dddd", new CultureInfo("vi-VN")),
                        Date = slot.StartTime.ToString("yyyy-MM-dd"),
                        BookedSlot = $"{slot.StartTime:HH\\:mm} - {slot.EndTime:HH\\:mm}"
                    });
                }
            }

            return new ApiSuccessResult<List<object>>(result);
        }


        public async Task<ApiResult<int>> GetLatestCoachBookingIdByPlayerAsync(int playerId)
        {
            var latestBooking = await _unitOfWork.GetRepository<CoachBooking>().Entities
                .Where(cb => cb.PlayerId == playerId)
                .OrderByDescending(cb => cb.CreatedTime)
                .FirstOrDefaultAsync();

            if (latestBooking == null)
                return new ApiErrorResult<int>("Không tìm thấy buổi đặt huấn luyện nào.");

            return new ApiSuccessResult<int>(latestBooking.Id);
        }
    }
}
