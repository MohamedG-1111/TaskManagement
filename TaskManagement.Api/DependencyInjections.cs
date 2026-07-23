using System.Text.Json.Serialization;

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
            services.AddSwaggerGen();

            services.AddProblemDetails();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            return services;
        }
    }
}
