using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Infrastructure.Persistence.Seeding;

namespace TaskManagement.Infrastructure.Extensions
{
    public static class DatabaseSeederExtensions
    {
        public static async Task SeedDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var seeder = scope.ServiceProvider
                .GetRequiredService<DataBaseSeeding>();

            await seeder.SeedAllAsync();
        }
    }
}
