using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.Contract.Repositories.Interface;
using TeamUp.Core.Utils;

public class RoomPlayerWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<RoomPlayerWorker> _logger;

    public RoomPlayerWorker(IServiceProvider serviceProvider, ILogger<RoomPlayerWorker> logger)
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
                    var now = DateTime.Now;
                    var twoHoursLater = now.AddHours(2);

                    var roomRepo = unitOfWork.GetRepository<Room>();
                    var roomPlayerRepo = unitOfWork.GetRepository<RoomPlayer>();

                    // Lấy các Room sắp diễn ra trong 2 tiếng tới
                    var upcomingRooms = await roomRepo.Entities
                        .Where(r =>
                            !r.DeletedTime.HasValue &&
                            r.ScheduledTime >= now &&
                            r.ScheduledTime <= twoHoursLater)
                        .ToListAsync(stoppingToken);

                    foreach (var room in upcomingRooms)
                    {
                        // Lấy các người chơi đã accepted và chưa được gửi mail
                        var playersToNotify = await roomPlayerRepo.Entities
                            .Include(rp => rp.Player)
                            .Where(rp =>
                                rp.RoomId == room.Id &&
                                rp.Status == "Accepted" &&
                                !rp.IsNotified &&
                                !rp.DeletedTime.HasValue)
                            .ToListAsync(stoppingToken);

                        foreach (var rp in playersToNotify)
                        {
                            var email = rp.Player.Email;

                            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormSendEmail", "NotifyRoomStartingSoon.html");
                            if (!File.Exists(path))
                            {
                                _logger.LogWarning("Không tìm thấy file NotifyRoomStartingSoon.html");
                                continue;
                            }

                            var content = File.ReadAllText(path);
                            content = content.Replace("{{PlayerName}}", email); // hoặc tên thật nếu bạn có
                            content = content.Replace("{{RoomTitle}}", room.Name);
                            content = content.Replace("{{ScheduledTime}}", room.ScheduledTime.ToString("HH:mm dd/MM/yyyy"));

                            var result = DoingMail.SendMail(
                                "TeamUp",
                                "Phòng bạn tham gia sắp bắt đầu",
                                content,
                                email);

                            if (result)
                            {
                                rp.IsNotified = true;
                            }
                            else
                            {
                                _logger.LogWarning($"Gửi email cho {email} thất bại.");
                            }
                        }
                    }

                    await unitOfWork.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi trong RoomPlayerWorker.");
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
