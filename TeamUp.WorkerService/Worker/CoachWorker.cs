using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TeamUp.Contract.Repositories.Interface;
using TeamUp.Core.Utils;
using TeamUp.Repositories.Entity;

public class CoachWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<CoachWorker> _logger;

    public CoachWorker(IServiceProvider serviceProvider, ILogger<CoachWorker> logger)
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
                var userRepo = unitOfWork.GetRepository<ApplicationUser>();

                var now = DateTime.Now;

                var expiredCoaches = await userRepo.Entities
                    .Include(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                    .Where(u => !u.DeletedTime.HasValue &&
                                u.ExpireDate.HasValue &&
                                u.ExpireDate < now &&
                                u.StatusForCoach != "InActive" &&
                                u.UserRoles.Any(ur => ur.Role.Name == "Coach"))
                    .ToListAsync(stoppingToken);

                foreach (var coach in expiredCoaches)
                {
                    coach.StatusForCoach = "InActive";
                    coach.PackageId = null;
                    coach.Package = null;
                    coach.StartDate = null;
                    coach.ExpireDate = null;

                    // Optional: Send email notification to coach
                    var emailPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormSendEmail", "CoachPackageExpired.html");
                    if (File.Exists(emailPath))
                    {
                        var content = File.ReadAllText(emailPath)
                            .Replace("{{Name}}", coach.FullName ?? coach.Email);

                        DoingMail.SendMail(
                            "TeamUp",
                            "Gói huấn luyện viên của bạn đã hết hạn",
                            content,
                            coach.Email
                        );
                    }
                }

                await unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi CoachWorker");
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
