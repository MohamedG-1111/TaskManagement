using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using TaskManagement.Infrastructure.Identity;
using TaskManagement.Infrastructure.Persistence.Context;

namespace TaskManagement.Api
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                 {
                     options.JsonSerializerOptions.Converters.Add(
                         new JsonStringEnumConverter());
                 });
            // Discovers Minimal API endpoints and exposes metadata for OpenAPI/Swagger.
            services.AddEndpointsApiExplorer();

            // Generates the OpenAPI (Swagger) document used by Swagger UI.
            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
            });

            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<IdentityAppDbContext>()
                .AddDefaultTokenProviders();

            services.AddProblemDetails();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            return services;
        }
    }
}
