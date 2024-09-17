
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using AppointMeWeb.Infrastrucure.Data.Models;
using AppointMeWeb.Infrastrucure.Data;
using AppointMeWeb.Core.Contracts;
using AppointMeWeb.Core.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<ICustomUserService, CustomUserService>();

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
