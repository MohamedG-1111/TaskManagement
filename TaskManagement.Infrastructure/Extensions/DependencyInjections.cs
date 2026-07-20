using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Abstractions.Identity;
using TaskManagement.Infrastructure.Persistence.Context;
using TaskManagement.Infrastructure.Persistence.Seeding;

namespace TaskManagement.Infrastructure.Extensions
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, WebApplication app,
            IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    config.GetConnectionString("DefaultConnection")));

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, Identity.CurrentUserService>();
            services.AddScoped<IDataSeeder, ProjectSeeder>();
            services.AddScoped<DataBaseSeeding>();


            return services;
        }
    }
}
