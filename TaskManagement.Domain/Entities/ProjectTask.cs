using TaskManagement.Domain.Common;
using TaskManagement.Domain.Enums;
using TaskStatus = TaskManagement.Domain.Enums.TaskStatus;

namespace TaskManagement.Domain.Entities
{
    public class ProjectTask : BaseEntity
    {
        public string Title { get; private set; } = null!;

        public string? Description { get; private set; }

        public Priority Priority { get; private set; }

        public TaskStatus Status { get; private set; }

        public DateTimeOffset DueDate { get; private set; }

        public Guid ProjectId { get; private set; }

        public Project Project { get; private set; } = null!;

        public Guid AssignedUserId { get; private set; }
    }
}
