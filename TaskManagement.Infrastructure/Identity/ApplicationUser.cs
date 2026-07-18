using Microsoft.AspNetCore.Identity;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; } = null!;

        public string Role { get; set; } = null!;

        public List<Project>? ManagedProjects { get; set; } = new List<Project>();
        public List<ProjectTask>? UserTasks { get; set; } = new List<ProjectTask>();
    }
}
