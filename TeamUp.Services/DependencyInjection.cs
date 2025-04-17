using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeamUp.Contract.Repositories.Interface;
using TeamUp.Repositories.UOW;

namespace TeamUp.Services
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories();
        }
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
