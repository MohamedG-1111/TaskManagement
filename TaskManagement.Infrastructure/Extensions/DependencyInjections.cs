using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Abstractions.Identity;
using TaskManagement.Domain.Repositories;
using TaskManagement.Infrastructure.Identity;
using TaskManagement.Infrastructure.Persistence.Context;
using TaskManagement.Infrastructure.Persistence.Interceptors;
using TaskManagement.Infrastructure.Persistence.Seeding;
using TaskManagement.Infrastructure.Repositories;

namespace TaskManagement.Infrastructure.Extensions
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddScoped<AuditInterceptor>();
            services.AddScoped<SoftDeleteInterceptor>();

            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

            services.AddDbContext<AppDbContext>((sp, options) =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
                options.AddInterceptors(
      sp.GetRequiredService<AuditInterceptor>(),
      sp.GetRequiredService<SoftDeleteInterceptor>());
            });


            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IDataSeeder, ProjectSeeder>();
            services.AddScoped<RoleSeeder>();
            services.AddScoped<ApplicationUserSeeder>();
            services.AddScoped<DataBaseSeeding>();


            return services;
        }
    }
}
