using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using TaskManagement.Infrastructure.Identity;
using TaskManagement.Infrastructure.Persistence.Context;

namespace TaskManagement.Api.Extensions
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddPresentation(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(
                        new JsonStringEnumConverter());
                });

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
            });

            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<IdentityAppDbContext>()
                .AddDefaultTokenProviders();

            services.AddProblemDetails();
            services.AddExceptionHandler<GlobalExceptionHandler>();

            services.AddJwtAuthentication(configuration);

            return services;
        }
    }
}
