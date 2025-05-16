using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.Contract.Repositories.Interface;
using TeamUp.Contract.Services.Interface;
using TeamUp.Core.APIResponse;
using TeamUp.ModelViews.PaymentModelViews;
using TeamUp.Core.Utils.VNPay;
using TeamUp.ModelViews.SportsComplexModelViews;
using TeamUp.ModelViews.UserModelViews.Response;
using TeamUp.ModelViews.CourtBookingModelViews;
using TeamUp.ModelViews.CourtModelViews;
using TeamUp.ModelViews.CoachBookingModelViews;
using TeamUp.Core;
using VNPAY.NET;
using VNPAY.NET.Enums;
using VNPAY.NET.Models;
using BabyCare.Core.Utils;
using TeamUp.Repositories.Entity;
using static BabyCare.Core.Utils.SystemConstant;

namespace TeamUp.Services.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IVnpay _vnpay;

        private string _tmnCode;
        private string _hashSecret;
        private string _baseUrl;
        private string _callbackUrl;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor, IConfiguration configuration, IVnpay vnpay)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _configuration = configuration;
            _vnpay = vnpay;

            _tmnCode = configuration["Vnpay:TmnCode"];
            _hashSecret = configuration["Vnpay:HashSecret"];
            _baseUrl = configuration["Vnpay:BaseUrl"];
            _callbackUrl = configuration["Vnpay:ReturnUrl"];

            _vnpay.Initialize(_tmnCode, _hashSecret, _baseUrl, _callbackUrl);
        }

        public async Task<ApiResult<string>> CreateVNPayPaymentUrlAsync(CreatePaymentModelView model, string ipAddress)
        {
            // Kiểm tra dữ liệu
            if (model.CourtBookingId == null && model.CoachBookingId == null && model.PackageId == null)
                return new ApiErrorResult<string>("Invalid payment information");

            decimal amount = 0;
            string description;

            if (model.CourtBookingId.HasValue)
            {
                var booking = await _unitOfWork.GetRepository<CourtBooking>()
                    .Entities.FirstOrDefaultAsync(x => x.Id == model.CourtBookingId && !x.DeletedTime.HasValue);

                if (booking == null)
                    return new ApiErrorResult<string>("Không tìm thấy đặt sân.");

                amount = booking.TotalPrice;
                description = $"{model.UserId}|Court|{booking.Id}";
            }
            else if (model.CoachBookingId.HasValue)
            {
                var booking = await _unitOfWork.GetRepository<CoachBooking>()
                    .Entities.FirstOrDefaultAsync(x => x.Id == model.CoachBookingId && !x.DeletedTime.HasValue);

                if (booking == null)
                    return new ApiErrorResult<string>("Không tìm thấy đặt huấn luyện viên.");

                amount = booking.TotalPrice;
                description = $"{model.UserId}|Coach|{booking.Id}";
            }
            else if (model.PackageId.HasValue)
            {
                var package = await _unitOfWork.GetRepository<Package>()
                    .Entities.FirstOrDefaultAsync(x => x.Id == model.PackageId && !x.DeletedTime.HasValue);

                if (package == null)
                    return new ApiErrorResult<string>("Không tìm thấy gói bạn cần.");

                amount = package.Price;
                description = $"{model.UserId}|{package.Type}|{package.Id}";
            }
            else
            {
                return new ApiErrorResult<string>("Phải chọn đặt sân hoặc huấn luyện viên.");
            }

            var request = new PaymentRequest
            {
                PaymentId = DateTime.Now.Ticks,
                Money = (double)amount,
                Description = description,
                IpAddress = ipAddress,
                BankCode = BankCode.ANY,
                CreatedDate = DateTime.Now,
                Currency = Currency.VND,
                Language = DisplayLanguage.Vietnamese
            };

            string paymentUrl = _vnpay.GetPaymentUrl(request);
            return new ApiSuccessResult<string>(paymentUrl);
        }

        public async Task<ApiResult<object>> HandleVNPayReturnAsync(IQueryCollection vnpParams)
        {
            var result = _vnpay.GetPaymentResult(vnpParams);
            if (result == null || string.IsNullOrEmpty(result.Description))
                return new ApiErrorResult<object>("Phản hồi thanh toán không hợp lệ.");

            var parts = result.Description.Split('|');
            if (parts.Length != 3)
                return new ApiErrorResult<object>("Định dạng mô tả thanh toán không hợp lệ.");

            int userId = int.Parse(parts[0]);
            string type = parts[1];
            int bookingId = int.Parse(parts[2]);

            _unitOfWork.BeginTransaction();

            try
            {
                Payment payment = new Payment
                {
                    CreatedTime = DateTime.Now,
                    CreatedBy = userId,
                    Method = "VNPay",
                    UserId = userId,
                    PaymentDate = DateTime.Now,
                    Status = result.IsSuccess ? "Success" : "Failed"
                };

                if (type == "Court")
                {
                    var booking = await _unitOfWork.GetRepository<CourtBooking>()
                        .GetByIdAsync(bookingId);
                    if (booking == null)
                        return new ApiErrorResult<object>("Không tìm thấy thông tin đặt sân.");

                    booking.Status = result.IsSuccess
                        ? SystemConstant.BookingStatus.Completed
                        : SystemConstant.BookingStatus.Failed;

                    booking.PaymentStatus = result.IsSuccess
                        ? SystemConstant.PaymentStatus.Paid
                        : SystemConstant.PaymentStatus.Failed;

                    _unitOfWork.GetRepository<CourtBooking>().Update(booking);

                    payment.Amount = booking.TotalPrice;
                    payment.CourtBookingId = bookingId;

                }
                else if (type == "Coach")
                {
                    var booking = await _unitOfWork.GetRepository<CoachBooking>()
                        .GetByIdAsync(bookingId);
                    if (booking == null)
                        return new ApiErrorResult<object>("Không tìm thấy thông tin đặt huấn luyện viên.");

                    booking.Status = result.IsSuccess
                        ? SystemConstant.BookingStatus.Completed
                        : SystemConstant.BookingStatus.Failed;

                    booking.PaymentStatus = result.IsSuccess
                        ? SystemConstant.PaymentStatus.Paid
                        : SystemConstant.PaymentStatus.Failed;

                    _unitOfWork.GetRepository<CoachBooking>().Update(booking);

                    payment.Amount = booking.TotalPrice;
                    payment.CoachBookingId = bookingId;

                }
                else
                {
                    var user = await _unitOfWork.GetRepository<ApplicationUser>()
                        .GetByIdAsync(userId);
                    if (user == null)
                        return new ApiErrorResult<object>("Không tìm thấy thông tin người chơi.");

                    var package = await _unitOfWork.GetRepository<Package>()
                        .GetByIdAsync(bookingId);
                    if (package == null)
                        return new ApiErrorResult<object>("Không tìm thấy thông tin gói.");

                    user.StartDate = DateTime.Now;

                    user.ExpireDate = DateTime.Now.AddDays(package.DurationDays);

                    _unitOfWork.GetRepository<ApplicationUser>().Update(user);

                    var userRoleRepo = _unitOfWork.GetRepository<ApplicationUserRole>();
                    var roleRepo = _unitOfWork.GetRepository<ApplicationRole>();

                    var userRole = await userRoleRepo.Entities
                        .Where(ur => ur.UserId == user.Id)
                        .Select(ur => ur.RoleId)
                        .FirstOrDefaultAsync();

                    var role = await roleRepo.Entities
                        .Where(r => r.Id == userRole)
                        .Select(r => r.Name)
                        .FirstOrDefaultAsync();

                    if (string.IsNullOrEmpty(role))
                        return new ApiErrorResult<object>("Không xác định được vai trò của người dùng.");

                    if (role == "Owner")
                    {
                        var sportsComplexRepo = _unitOfWork.GetRepository<SportsComplex>();
                        var courtRepo = _unitOfWork.GetRepository<Court>();

                        // Lấy danh sách khu thể thao của user
                        var complexes = await sportsComplexRepo.Entities
                            .Where(x => x.OwnerId == user.Id && !x.DeletedTime.HasValue)
                            .ToListAsync();

                        foreach (var complex in complexes)
                        {
                            complex.Status = PackageStatus.Active;
                            sportsComplexRepo.Update(complex);

                            // Lấy và cập nhật sân trong khu thể thao
                            var courts = await courtRepo.Entities
                                .Where(c => c.SportsComplexId == complex.Id && !c.DeletedTime.HasValue)
                                .ToListAsync();

                            foreach (var court in courts)
                            {
                                court.Status = PackageStatus.Active; 
                                courtRepo.Update(court);
                            }
                        }
                    } 
                    else if (role == "Coach")
                    {
                        user.StatusForCoach = PackageStatus.Active;

                        _unitOfWork.GetRepository<ApplicationUser>().Update(user);
                    }

                    payment.Amount = package.Price;
                    payment.PackageId = bookingId;
                }

                await _unitOfWork.GetRepository<Payment>().InsertAsync(payment);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();

                return new ApiSuccessResult<object>("Xử lý thanh toán thành công.");
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                return new ApiErrorResult<object>($"Lỗi xử lý thanh toán: {ex.Message}");
            }
        }



        public async Task<ApiResult<PaymentModelView>> GetPaymentByIdAsync(int id)
        {
            var payment = await _unitOfWork.GetRepository<Payment>().Entities
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id && !p.DeletedTime.HasValue);

            if (payment == null)
                return new ApiErrorResult<PaymentModelView>("Không tìm thấy thanh toán.");

            var result = _mapper.Map<PaymentModelView>(payment);

            result.User = _mapper.Map<UserResponseModel>(payment.User);

            if (payment.CourtBooking != null)
            {
                result.CourtBooking = _mapper.Map<CourtBookingModelView>(payment.CourtBooking);

                result.CourtBooking.Court = _mapper.Map<CourtModelView>(payment.CourtBooking.Court);

                result.CourtBooking.Court.SportsComplexModelView = _mapper.Map<SportsComplexModelView>(payment.CourtBooking.Court.SportsComplex);
            }

            if (payment.CoachBooking != null)
            {
                result.CoachBooking = _mapper.Map<CoachBookingModelView>(payment.CoachBooking);

                result.CoachBooking.Court = _mapper.Map<CourtModelView>(payment.CoachBooking.Court);

                result.CoachBooking.Court.SportsComplexModelView = _mapper.Map<SportsComplexModelView>(payment.CoachBooking.Court.SportsComplex);
            }

            return new ApiSuccessResult<PaymentModelView>(result);
        }

        public async Task<ApiResult<BasePaginatedList<PaymentModelView>>> GetAllPaymentsAsync(int pageNumber, int pageSize, int? userId)
        {
            var query = _unitOfWork.GetRepository<Payment>().Entities
                .Include(p => p.User)
                .Include(p => p.CourtBooking).ThenInclude(cb => cb.Court).ThenInclude(c => c.SportsComplex)
                .Include(p => p.CoachBooking).ThenInclude(cb => cb.Court).ThenInclude(c => c.SportsComplex)
                .Where(p => !p.DeletedTime.HasValue);

            if (userId.HasValue)
            {
                query = query.Where(p => p.UserId == userId.Value);
            }

            int totalCount = await query.CountAsync();

            var paginatedPayments = await query
                .OrderByDescending(p => p.PaymentDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = _mapper.Map<List<PaymentModelView>>(paginatedPayments);

            for (int i = 0; i < result.Count; i++)
            {
                var payment = paginatedPayments[i];
                result[i].User = _mapper.Map<UserResponseModel>(payment.User);

                if (payment.CourtBooking != null)
                {
                    result[i].CourtBooking = _mapper.Map<CourtBookingModelView>(payment.CourtBooking);
                    result[i].CourtBooking.Court = _mapper.Map<CourtModelView>(payment.CourtBooking.Court);
                    result[i].CourtBooking.Court.SportsComplexModelView = _mapper.Map<SportsComplexModelView>(payment.CourtBooking.Court.SportsComplex);
                }

                if (payment.CoachBooking != null)
                {
                    result[i].CoachBooking = _mapper.Map<CoachBookingModelView>(payment.CoachBooking);
                    result[i].CoachBooking.Court = _mapper.Map<CourtModelView>(payment.CoachBooking.Court);
                    result[i].CoachBooking.Court.SportsComplexModelView = _mapper.Map<SportsComplexModelView>(payment.CoachBooking.Court.SportsComplex);
                }
            }

            return new ApiSuccessResult<BasePaginatedList<PaymentModelView>>(
                new BasePaginatedList<PaymentModelView>(result, totalCount, pageNumber, pageSize));
        }
    }
}
