namespace TaskManagement.Application.Abstractions.Identity
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }

        string? UserName { get; }

        string? Email { get; }

        IReadOnlyCollection<string> Roles { get; }

        bool IsAuthenticated { get; }

        bool IsInRole(string roleName);
    }
}

