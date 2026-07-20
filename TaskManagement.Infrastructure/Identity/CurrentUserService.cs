using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TaskManagement.Application.Abstractions.Identity;

namespace TaskManagement.Infrastructure.Identity;

public sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _contextAccessor;
    private ClaimsPrincipal? User => _contextAccessor.HttpContext?.User;
    public CurrentUserService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);

            return Guid.TryParse(userId, out var id)
                ? id
                : Guid.Empty;
        }
    }

    public string? UserName =>
        User?.FindFirstValue(ClaimTypes.Name);

    public string? Email =>
        User?.FindFirstValue(ClaimTypes.Email);

    public IReadOnlyCollection<string> Roles =>
        User?
            .FindAll(ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList()
        ?? [];

    public bool IsAuthenticated =>
        User?.Identity?.IsAuthenticated ?? false;

    public bool IsInRole(string roleName) =>
        User?.IsInRole(roleName) ?? false;
}