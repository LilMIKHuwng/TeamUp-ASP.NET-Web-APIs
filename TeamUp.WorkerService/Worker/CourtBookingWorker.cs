using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.Contract.Repositories.Interface;
using TeamUp.Core.Utils;

public class CourtBookingWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<CourtBookingWorker> _logger;

    public CourtBookingWorker(IServiceProvider serviceProvider, ILogger<CourtBookingWorker> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    var bookingRepo = unitOfWork.GetRepository<CourtBooking>();
                    var now = DateTime.Now;
                    var twoHoursLater = now.AddHours(2);

                    var bookings = await bookingRepo.Entities
                        .Include(b => b.User)
                        .Include(b => b.Court)
                        .Where(b =>
                            !b.DeletedTime.HasValue &&
                            b.StartTime >= now &&
                            b.StartTime <= twoHoursLater &&
                            b.Status == "Confirmed" &&
                            !b.IsNotified)
                        .ToListAsync(stoppingToken);

                    foreach (var booking in bookings)
                    {
                        var email = booking.User.Email;

                        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormSendEmail", "NotifyCourtBookingStartingSoon.html");
                        if (!File.Exists(path))
                        {
                            _logger.LogWarning("Không tìm thấy file NotifyCourtBookingStartingSoon.html");
                            continue;
                        }

                        var content = File.ReadAllText(path);
                        content = content.Replace("{{UserName}}", email); // hoặc tên thật nếu có
                        content = content.Replace("{{CourtName}}", booking.Court.Name);
                        content = content.Replace("{{StartTime}}", booking.StartTime.ToString("HH:mm dd/MM/yyyy"));
                        content = content.Replace("{{EndTime}}", booking.EndTime.ToString("HH:mm dd/MM/yyyy"));

                        var result = DoingMail.SendMail(
                            "TeamUp",
                            "Lịch đặt sân của bạn sắp bắt đầu",
                            content,
                            email);

                        if (result)
                        {
                            booking.IsNotified = true;
                        }
                        else
                        {
                            _logger.LogWarning($"Gửi email thất bại: {email}");
                        }
                    }

                    await unitOfWork.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi trong CourtBookingWorker.");
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
