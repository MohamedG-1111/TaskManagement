namespace TaskManagement.Domain.Contants;

public static class ApplicationRoles
{
    public const string Admin = "Admin";
    public const string SuperAdmin = "SuperAdmin";
    public const string Employee = "Employee";

    public static readonly IReadOnlyList<string> All = new[]
      {
            ApplicationRoles.Admin,
            ApplicationRoles.SuperAdmin,
            ApplicationRoles.Employee
      };
}
