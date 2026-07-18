using TaskManagement.Domain.Common;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; private set; } = null!;

        public string Description { get; private set; } = null!;

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public ProjectStatus Status { get; set; }

        public Guid ManagerId { get; set; }

        public List<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();

    }
}
