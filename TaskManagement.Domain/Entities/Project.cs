using TaskManagement.Domain.Common.Baseentity;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities
{
    public class Project : BaseEntity
    {
        private const int NameMaxLength = 100;
        private const int DescriptionMaxLength = 500;

        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public DateTimeOffset StartDate { get; private set; }
        public DateTimeOffset EndDate { get; private set; }
        public ProjectStatus Status { get; private set; }
        public Guid ManagerId { get; private set; }


        private Project(string name, string description, DateTimeOffset startDate,
            DateTimeOffset endDate, Guid managerId)
        {
            SetName(name);
            SetDescription(description);
            StartDate = startDate;
            SetEndDate(endDate, maxTaskDueDate: null);
            ManagerId = managerId;
            Status = ProjectStatus.Planning;
        }

        public static Project Create(string name, string description, DateTimeOffset startDate,
            DateTimeOffset endDate, Guid managerId)
        {
            return new Project(name, description, startDate, endDate, managerId);
        }

        // ---------- Basic Details (blocked once completed/archived) ----------

        public void UpdateDetails(string name, string description, DateTimeOffset startDate,
            DateTimeOffset endDate, DateTimeOffset? maxTaskDueDate)
        {
            EnsureNotCompleted();

            SetName(name);
            SetDescription(description);
            StartDate = startDate;
            SetEndDate(endDate, maxTaskDueDate);
        }

        private void SetName(string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            if (name.Length > NameMaxLength)
                throw new InvalidOperationException(
                    $"Project name cannot exceed {NameMaxLength} characters.");

            Name = name;
        }

        private void SetDescription(string description)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(description);

            if (description.Length > DescriptionMaxLength)
                throw new InvalidOperationException(
                    $"Project description cannot exceed {DescriptionMaxLength} characters.");

            Description = description;
        }


        private void SetEndDate(DateTimeOffset endDate, DateTimeOffset? maxTaskDueDate)
        {
            if (endDate <= StartDate)
                throw new InvalidOperationException("End date must be after the start date.");

            if (maxTaskDueDate.HasValue && maxTaskDueDate.Value > endDate)
                throw new InvalidOperationException(
                    "End date cannot be earlier than the due date of any task in the project.");

            EndDate = endDate;
        }

        public void ChangeManager(Guid newManagerId, bool hasStartedTasks)
        {
            EnsureNotCompleted();

            if (hasStartedTasks)
                throw new InvalidOperationException(
                    "Cannot change manager after a task has started execution.");

            ManagerId = newManagerId;
        }


        public void EnsureCanAddTask(
     DateTimeOffset taskStartDate,
     DateTimeOffset taskDueDate)
        {
            if (Status is ProjectStatus.Completed or ProjectStatus.Archived)
                throw new InvalidOperationException(
                    "Cannot add tasks to a completed or archived project.");

            if (taskStartDate < StartDate)
                throw new InvalidOperationException(
                    "Task start date cannot be before the project start date.");

            if (taskDueDate > EndDate)
                throw new InvalidOperationException(
                    "Task due date cannot be after the project end date.");

            if (taskDueDate <= taskStartDate)
                throw new InvalidOperationException(
                    "Task due date must be after its start date.");
        }

        public void EnsureCanModifyTasks()
        {
            if (Status is ProjectStatus.Completed or ProjectStatus.Archived)
                throw new InvalidOperationException("Cannot modify tasks in a completed or archived project.");
        }

        // ---------- Status Transitions ----------

        public void Activate()
        {
            if (Status != ProjectStatus.Planning)
                throw new InvalidOperationException("Only a Planning project can be activated.");

            Status = ProjectStatus.Active;
        }

        /// <summary>hasIncompleteTasks is resolved by the caller via an EXISTS query.</summary>
        public void Complete(bool hasIncompleteTasks)
        {
            if (Status != ProjectStatus.Active)
                throw new InvalidOperationException("Only active projects can be completed.");

            if (hasIncompleteTasks)
                throw new InvalidOperationException(
                    "Cannot complete the project while it still has pending or in-progress tasks.");

            Status = ProjectStatus.Completed;
        }

        public void Archive()
        {
            if (Status != ProjectStatus.Completed)
                throw new InvalidOperationException("Only completed projects can be archived.");

            Status = ProjectStatus.Archived;
        }
        public bool CanBeDeleted(bool hasTasks) => !hasTasks;

        private void EnsureNotCompleted()
        {
            if (Status is ProjectStatus.Completed or ProjectStatus.Archived)
                throw new InvalidOperationException("Cannot modify a completed or archived project.");
        }
    }
}