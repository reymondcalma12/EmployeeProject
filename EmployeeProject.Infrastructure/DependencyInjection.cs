using EmployeeProject.Core.Interfaces;
using EmployeeProject.Infrastructure.Data;
using EmployeeProject.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeProject.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection") ??
                    throw new InvalidOperationException("Connection String is not found"));
            });
            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();

            return services;
        }
    }
}
