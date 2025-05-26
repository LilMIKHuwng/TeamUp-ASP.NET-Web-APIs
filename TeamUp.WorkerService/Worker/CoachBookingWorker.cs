using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.Contract.Repositories.Interface;
using TeamUp.Core.Utils;
using System.Globalization;

public class CoachBookingWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<CoachBookingWorker> _logger;

    public CoachBookingWorker(IServiceProvider serviceProvider, ILogger<CoachBookingWorker> logger)
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
                using var scope = _serviceProvider.CreateScope();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var coachBookingRepo = unitOfWork.GetRepository<CoachBooking>();

                var now = DateTime.Now;

                var bookings = await coachBookingRepo.Entities
                    .Include(cb => cb.Player)
                    .Include(cb => cb.Coach)
                    .Include(cb => cb.Court)
                    .Include(cb => cb.Slots)
                    .Where(cb => !cb.DeletedTime.HasValue &&
                                 cb.Status == "Confirmed" &&
                                 !cb.IsNotified &&
                                 cb.Slots.Any())
                    .ToListAsync(stoppingToken);

                foreach (var booking in bookings)
                {
                    var upcomingSlot = booking.Slots
                        .OrderBy(s => s.StartTime)
                        .FirstOrDefault(s =>
                            s.StartTime > now &&
                            (s.StartTime - now).TotalHours <= 2);

                    if (upcomingSlot != null)
                    {
                        var emailPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormSendEmail", "NotifyCoachBooking.html");
                        if (!File.Exists(emailPath)) continue;

                        // Tạo bảng các buổi huấn luyện
                        var slotRows = string.Join("", booking.Slots
                            .OrderBy(s => s.StartTime)
                            .Select((s, index) => $@"
                            <tr>
                                <td style='padding: 6px 12px;'>{index + 1}</td>
                                <td style='padding: 6px 12px;'>{s.StartTime:dd/MM/yyyy}</td>
                                <td style='padding: 6px 12px;'>{s.StartTime:HH:mm}</td>
                                <td style='padding: 6px 12px;'>{s.EndTime:HH:mm}</td>
                            </tr>"));

                        var content = File.ReadAllText(emailPath)
                            .Replace("{{UserEmail}}", booking.Player.Email)
                            .Replace("{{CoachEmail}}", booking.Coach.Email)
                            .Replace("{{CourtName}}", booking.Court.Name)
                            .Replace("{{SlotItems}}", slotRows)
                            .Replace("{{Status}}", booking.Status);

                        var sent = DoingMail.SendMail(
                            "TeamUp",
                            "Nhắc nhở buổi huấn luyện sắp tới",
                            content,
                            booking.Player.Email);

                        if (sent)
                        {
                            booking.IsNotified = true;
                            await unitOfWork.SaveAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi CoachBookingWorker");
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }


}
