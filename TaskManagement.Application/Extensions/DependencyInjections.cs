using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Common.Behaviors;

namespace TaskManagement.Application.Extensions
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(
                    typeof(ApplicationAssemblyReference).Assembly);
            });

            services.AddValidatorsFromAssembly(
                typeof(ApplicationAssemblyReference).Assembly);

            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));

            return services;
        }
    }
}