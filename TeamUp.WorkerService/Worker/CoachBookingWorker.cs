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
                    .Where(cb => !cb.DeletedTime.HasValue &&
                                 cb.Status == "Confirmed" &&
                                 !cb.IsNotified &&
                                 cb.SelectedDates.Any())
                    .ToListAsync(stoppingToken);

                foreach (var booking in bookings)
                {
                    var firstSession = booking.SelectedDates.Min();
                    var fullFirstSessionTime = firstSession.Date + booking.StartTime;
                    var timeUntilFirst = fullFirstSessionTime - now;

                    if (timeUntilFirst.TotalHours <= 2 && timeUntilFirst.TotalMinutes > 0)
                    {
                        // Load template
                        var emailPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormSendEmail", "NotifyCoachBooking.html");
                        if (!File.Exists(emailPath)) continue;

                        var sessionsListHtml = string.Join("", booking.SelectedDates
                            .OrderBy(d => d)
                            .Select(d => $"<li>{d:dd/MM/yyyy} - {booking.StartTime:hh\\:mm} đến {booking.EndTime:hh\\:mm}</li>"));

                        var content = File.ReadAllText(emailPath)
                            .Replace("{{UserEmail}}", booking.Player.Email)
                            .Replace("{{CoachEmail}}", booking.Coach.Email)
                            .Replace("{{CourtName}}", booking.Court.Name)
                            .Replace("{{Status}}", booking.Status)
                            .Replace("{{SessionsList}}", sessionsListHtml);

                        var sent = DoingMail.SendMail(
                            "TeamUp",
                            "Lịch học huấn luyện sắp tới",
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
