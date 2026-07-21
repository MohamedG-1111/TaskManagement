using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Infrastructure.Persistence.Context;
using TaskManagement.Infrastructure.Persistence.Seeding;

namespace TaskManagement.Infrastructure.Extensions;

public static class DatabaseSeederExtensions
{
    public static async Task SeedDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var services = scope.ServiceProvider;

        var dbContext = services.GetRequiredService<AppDbContext>();

        await dbContext.Database.MigrateAsync();

        var roleSeeder = services.GetRequiredService<RoleSeeder>();
        await roleSeeder.SeedAsync();

        var userSeeder = services.GetRequiredService<ApplicationUserSeeder>();
        await userSeeder.SeedAsync();

        var databaseSeeder = services.GetRequiredService<DataBaseSeeding>();
        await databaseSeeder.SeedAllAsync();
    }
}