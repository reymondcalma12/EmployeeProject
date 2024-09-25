using EmployeeProject.Core.Entities.User;
using EmployeeProject.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Identity;
using EmployeeProject.Infrastructure;
using Microsoft.EntityFrameworkCore;
using EmployeeProject.Application;
using EmployeeProject.Application.Hubs;
using Serilog;
using ExternalLogin.Interfaces;
using ExternalLogin.Services;
using ExternalLogin;

namespace EmployeeProject.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

            //AD Login
            // set the db connection
            var CentralLoginConnectionString = builder.Configuration.GetConnectionString("CentralLoginConnection")
            ?? throw new InvalidOperationException("Connection string 'ExternalDbContext' not found.");

            builder.Services.AddDbContext<ExternalDbContext>(options =>
                options.UseSqlServer(CentralLoginConnectionString));

            //external login
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IExternalLoginService, ExternalLoginService>();


            //builder.Services.AddInfrastructure(builder.Configuration);
            // Add services to the container.

            builder.Services.AddControllersWithViews();


            builder.Services.AddInfrastructure(builder.Configuration).AddApplication();

            builder.Services.AddIdentity<AppUser, IdentityRole>(
                    options =>
                    {
                        options.Password.RequiredUniqueChars = 0;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequiredLength = 8;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireDigit = false;                    }
            )
                .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //builder.Services.AddDbContext<AppDbContext>(options =>
            //  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSignalR();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            builder.Services.AddAuthorization(options =>
            {

                options.AddPolicy("RequireManager,ProjectManagerRoleOrAdmin", policy =>
                    policy.RequireRole("Manager", "Project_Manager", "Admin"));

                options.AddPolicy("RequireEmployeeRole", policy =>
                    policy.RequireRole("Employee"));

            });



            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthorization();
            app.UseWhen(context => !context.User.IsInRole("Manager") && context.Request.Path.StartsWithSegments("/Manager"),
                appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 403;
                        await context.Response.WriteAsync("You are not authorized to access the Manager controller.");
                    });
                });

            app.UseWhen(context => !context.User.IsInRole("Employee") && context.Request.Path.StartsWithSegments("/Employee"),
                appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 403;
                        await context.Response.WriteAsync("You are not authorized to access the Employee controller.");
                    });
                });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Login}/{id?}");
                endpoints.MapHub<AdminHub>("/adminHub");
            });


            app.Run();
        }
    }
}
