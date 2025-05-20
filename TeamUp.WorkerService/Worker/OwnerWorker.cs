using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TeamUp.Contract.Repositories.Interface;
using TeamUp.Core.Utils;
using TeamUp.Repositories.Entity;

public class OwnerWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OwnerWorker> _logger;

    public OwnerWorker(IServiceProvider serviceProvider, ILogger<OwnerWorker> logger)
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

                var expiredOwners = await userRepo.Entities
                    .Include(u => u.SportsComplexs)
                        .ThenInclude(sc => sc.Courts)
                    .Include(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                    .Where(u => !u.DeletedTime.HasValue &&
                                u.ExpireDate.HasValue &&
                                u.ExpireDate < now &&
                                u.Status != 0 &&
                                u.UserRoles.Any(ur => ur.Role.Name == "Owner"))
                    .ToListAsync(stoppingToken);

                foreach (var owner in expiredOwners)
                {
                    owner.PackageId = null;
                    owner.Package = null;
                    owner.StartDate = null;
                    owner.ExpireDate = null;

                    foreach (var sc in owner.SportsComplexs)
                    {
                        sc.Status = "InActive";
                        foreach (var court in sc.Courts)
                        {
                            court.Status = "InActive";
                        }
                    }

                    var emailPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormSendEmail", "OwnerPackageExpired.html");
                    if (!File.Exists(emailPath)) continue;

                    var content = File.ReadAllText(emailPath)
                        .Replace("{{Name}}", owner.Email);

                    DoingMail.SendMail(
                        "TeamUp",
                        "Gói dịch vụ của bạn đã hết hạn",
                        content,
                        owner.Email
                    );
                }

                await unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi OwnerWorker");
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
