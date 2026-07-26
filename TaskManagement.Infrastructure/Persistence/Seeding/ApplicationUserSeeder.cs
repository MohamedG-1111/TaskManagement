using Microsoft.AspNetCore.Identity;
using TaskManagement.Domain.Contants;
using TaskManagement.Infrastructure.Identity;

namespace TaskManagement.Infrastructure.Persistence.Seeding;

public class ApplicationUserSeeder
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationUserSeeder(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task SeedAsync()
    {
        if (_userManager.Users.Any())
            return;

        var users = new[]
        {
            CreateUser("1a2b3c4d-5e6f-4a1b-8c2d-3e4f5a6b7c8d", "Ahmed Ali", "ahmed", "ahmed@test.com", ApplicationRoles.Admin),
            CreateUser("2b3c4d5e-6f70-4b2c-9d3e-4f5a6b7c8d9e", "Mohamed Hassan", "mohamed", "mohamed@test.com", ApplicationRoles.Employee),
            CreateUser("3c4d5e6f-7081-4c3d-0e4f-5a6b7c8d9e0f", "Sara Adel", "sara", "sara@test.com", ApplicationRoles.Employee),
            CreateUser("4d5e6f70-8192-4d4e-1f50-6b7c8d9e0f10", "Omar Khaled", "omar", "omar@test.com", ApplicationRoles.Employee),
            CreateUser("5e6f7081-9203-4e5f-2061-7c8d9e0f1021", "Mona Samir", "mona", "mona@test.com", ApplicationRoles.Employee),
            CreateUser("6f708192-0314-4f60-3072-8d9e0f102132", "Youssef Nabil", "youssef", "youssef@test.com", ApplicationRoles.Employee),
            CreateUser("70819203-1425-4071-4083-9e0f10213243", "Nada Mostafa", "nada", "nada@test.com", ApplicationRoles.Employee),
            CreateUser("81920314-2536-4182-5094-0f1021324354", "Mahmoud Tarek", "mahmoud", "mahmoud@test.com", ApplicationRoles.Employee),
            CreateUser("92031425-3647-4293-6105-102132435406", "Fatma Adel", "fatma", "fatma@test.com", ApplicationRoles.Employee),
            CreateUser("03142536-4758-4304-7216-021324354657", "Karim Essam", "karim", "karim@test.com", ApplicationRoles.Employee)
        };

        foreach (var user in users)
        {
            var result = await _userManager.CreateAsync(user, "P@ssw0rd123");

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            await _userManager.AddToRoleAsync(user, user.Role);
        }
    }

    private static ApplicationUser CreateUser(
        string id,
        string fullName,
        string userName,
        string email,
        string role)
    {
        return new ApplicationUser
        {
            Id = Guid.Parse(id),
            FullName = fullName,
            UserName = userName,
            Email = email,
            EmailConfirmed = true,
            Role = role
        };
    }
}