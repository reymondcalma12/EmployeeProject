using EmployeeProject.Application.Interfaces;
using EmployeeProject.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeProject.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //var assembly = typeof(DependencyInjection).Assembly;

            //services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
            //services.AddValidatorsFromAssembly(assembly);

            services.AddScoped<IUserAccount, AccountServices>();
            services.AddScoped<IAdminServices, AdminServices>();
            services.AddScoped<IEmployeeServices, EmployeeServices>();

            return services;
        }
    }
}
