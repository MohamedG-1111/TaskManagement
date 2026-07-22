using Microsoft.Extensions.DependencyInjection;

namespace TaskManagement.Application.Extensions
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            return services;
        }
    }
}
