
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using AppointMeWeb.Infrastrucure.Data.Models;
using AppointMeWeb.Infrastrucure.Data;
using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Services;
using AppointMeWeb.Infrastrucure.Data.Common;
using static AppointMeWeb.Infrastrucure.Constants.DataConstants;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<ICustomUserService, CustomUserService>();
            services.AddScoped<IFactory, Factory> ();
            services.AddScoped<IRepository, SqlRepository>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            return services;
        }

        public static IServiceCollection AddApplicationIdentity(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDefaultIdentity<ApplicationUser>(options => {
                    options.SignIn.RequireConfirmedAccount = true;

                    options.Password.RequireNonAlphanumeric=false;
                    options.Password.RequireLowercase=false;
                    options.Password.RequireUppercase=false;
                    options.Password.RequireDigit=false;
                })
                .AddRoles<IdentityRole<string>>()
                 .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AdminRole, policy =>
                {
                    policy.RequireRole(AdminRole);
                    policy.RequireRole(BusinessRole);
                    policy.RequireRole(WebUserRole);
                });
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/User/Login";
                options.LogoutPath = $"/User/Logout";
            });

            return services;
        }

        public static IServiceCollection AddAplicationDbContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config
                .GetConnectionString ("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));

            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }
        public static IServiceCollection AddAplicationMongoDb(this IServiceCollection services)
        {
            return services;
        }
    }
}
