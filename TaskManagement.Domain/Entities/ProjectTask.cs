using TaskManagement.Domain.Common.Baseentity;
using TaskManagement.Domain.Enums;
using TaskStatus = TaskManagement.Domain.Enums.TaskStatus;

namespace TaskManagement.Domain.Entities
{
    public class ProjectTask : BaseEntity
    {
        private const int TitleMaxLength = 100;
        private const int DescriptionMaxLength = 500;

        public string Title { get; private set; } = null!;
        public string? Description { get; private set; }

        public Priority Priority { get; private set; }

        public TaskStatus Status { get; private set; }

        public DateTimeOffset StartDate { get; private set; }

        public DateTimeOffset DueDate { get; private set; }

        public Guid ProjectId { get; private set; }
        public Project Project { get; private set; } = null!;

        public Guid AssignedUserId { get; private set; }

        private ProjectTask(
            string title,
            string? description,
            Priority priority,
            DateTimeOffset startDate,
            DateTimeOffset dueDate,
            Guid projectId,
            Guid assignedUserId)
        {
            SetTitle(title);
            SetDescription(description);
            SetDates(startDate, dueDate);

            Priority = priority;
            SetProjectId(projectId);
            SetAssignedUserId(assignedUserId);
            Status = TaskStatus.Pending;
        }

        public static ProjectTask Create(
            string title,
            string? description,
            Priority priority,
            DateTimeOffset startDate,
            DateTimeOffset dueDate,
            Guid projectId,
            Guid assignedUserId)
        {
            return new ProjectTask(
                title,
                description,
                priority,
                startDate,
                dueDate,
                projectId,
                assignedUserId);
        }

        public void UpdateDetails(
            string title,
            string? description,
            Priority priority,
            DateTimeOffset startDate,
            DateTimeOffset dueDate)
        {
            EnsureNotCompleted();

            SetTitle(title);
            SetDescription(description);
            SetDates(startDate, dueDate);

            Priority = priority;
        }
        private void SetProjectId(Guid projectId)
        {
            if (projectId == Guid.Empty)
                throw new ArgumentException("Project is required.");

            ProjectId = projectId;
        }
        private void SetTitle(string title)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(title);

            if (title.Length > TitleMaxLength)
                throw new InvalidOperationException(
                    $"Task title cannot exceed {TitleMaxLength} characters.");

            Title = title;
        }

        private void SetDescription(string? description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                Description = null;
                return;
            }

            if (description.Length > DescriptionMaxLength)
                throw new InvalidOperationException(
                    $"Task description cannot exceed {DescriptionMaxLength} characters.");

            Description = description;
        }

        private void SetDates(
            DateTimeOffset startDate,
            DateTimeOffset dueDate)
        {
            if (startDate == default)
                throw new ArgumentException("Start date is required.");

            if (dueDate == default)
                throw new ArgumentException("Due date is required.");

            if (startDate < DateTimeOffset.UtcNow)
                throw new InvalidOperationException(
                    "Start date cannot be in the past.");

            if (dueDate <= startDate)
                throw new InvalidOperationException(
                    "Due date must be after start date.");

            StartDate = startDate;
            DueDate = dueDate;
        }

        public void AssignTo(Guid userId)
        {
            EnsureNotCompleted();
            SetAssignedUserId(userId);
        }

        public void Reopen()
        {
            if (Status == TaskStatus.Cancelled)
                throw new InvalidOperationException(
                    "Cancelled tasks cannot be reopened.");

            if (Status != TaskStatus.InProgress)
                throw new InvalidOperationException(
                    "Only in-progress tasks can be reopened to pending.");

            Status = TaskStatus.Pending;
        }
        private void SetAssignedUserId(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Assigned user is required.");

            AssignedUserId = userId;
        }
        public void Start()
        {
            if (Status != TaskStatus.Pending)
                throw new InvalidOperationException(
                    "Only pending tasks can be started.");

            Status = TaskStatus.InProgress;
        }

        public void Complete()
        {
            if (Status != TaskStatus.InProgress)
                throw new InvalidOperationException(
                    "Only tasks in progress can be completed.");

            Status = TaskStatus.Completed;
        }

        public void Cancel()
        {
            if (Status == TaskStatus.Completed)
                throw new InvalidOperationException(
                    "Completed tasks cannot be cancelled.");

            Status = TaskStatus.Cancelled;
        }

        private void EnsureNotCompleted()
        {
            if (Status == TaskStatus.Completed)
                throw new InvalidOperationException(
                    "Completed tasks cannot be modified.");
        }
        public void EnsureCanDelete()
        {
            if (Status == TaskStatus.Completed)
                throw new InvalidOperationException(
                    "Completed tasks cannot be Deleted.");
        }

        public void ChangePriority(Priority prority)
        {
            EnsureNotCompleted();
            Priority = prority;
        }
        public void MoveToProject(Guid projectId)
        {
            if (Status != TaskStatus.Pending)
                throw new InvalidOperationException(
                    "Only pending tasks can be moved to another project.");

            SetProjectId(projectId);
        }


    }
}