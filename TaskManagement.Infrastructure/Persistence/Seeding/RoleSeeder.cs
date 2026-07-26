using Microsoft.AspNetCore.Identity;
using TaskManagement.Domain.Contants;

namespace TaskManagement.Infrastructure.Persistence.Seeding;

public class RoleSeeder
{
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public RoleSeeder(RoleManager<IdentityRole<Guid>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task SeedAsync()
    {


        foreach (var role in ApplicationRoles.All)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
        }
    }
}