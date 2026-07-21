using TaskManagement.Domain.Common.Baseentity;
using TaskManagement.Domain.Common.Results;
using TaskManagement.Domain.Enums;
using TaskManagement.Domain.Errors;

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
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            ManagerId = managerId;
            Status = ProjectStatus.Planning;
        }

        public static Result<Project> Create(string name, string description, DateTimeOffset startDate,
            DateTimeOffset endDate, Guid managerId)
        {
            var nameResult = ValidateName(name);
            if (nameResult.IsFailure)
                return Result<Project>.Failure(nameResult.Error!);

            var descriptionResult = ValidateDescription(description);
            if (descriptionResult.IsFailure)
                return Result<Project>.Failure(descriptionResult.Error!);

            var endDateResult = ValidateEndDate(endDate, startDate, maxTaskDueDate: null);
            if (endDateResult.IsFailure)
                return Result<Project>.Failure(endDateResult.Error!);

            var project = new Project(name, description, startDate, endDate, managerId);
            return Result<Project>.Success(project);
        }

        // ---------- Basic Details (blocked once completed/archived) ----------

        public Result UpdateDetails(string name, string description, DateTimeOffset startDate,
            DateTimeOffset endDate, DateTimeOffset? maxTaskDueDate)
        {
            var notCompletedResult = EnsureNotCompleted();
            if (notCompletedResult.IsFailure)
                return notCompletedResult;

            var nameResult = ValidateName(name);
            if (nameResult.IsFailure)
                return nameResult;

            var descriptionResult = ValidateDescription(description);
            if (descriptionResult.IsFailure)
                return descriptionResult;

            var endDateResult = ValidateEndDate(endDate, startDate, maxTaskDueDate);
            if (endDateResult.IsFailure)
                return endDateResult;

            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;

            return Result.Success();
        }

        private static Result ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure(ProjectErrors.NameRequired);

            if (name.Length > NameMaxLength)
                return Result.Failure(ProjectErrors.NameTooLong);

            return Result.Success();
        }

        private static Result ValidateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure(ProjectErrors.DescriptionRequired);

            if (description.Length > DescriptionMaxLength)
                return Result.Failure(ProjectErrors.DescriptionTooLong);

            return Result.Success();
        }

        private static Result ValidateEndDate(DateTimeOffset endDate, DateTimeOffset startDate,
            DateTimeOffset? maxTaskDueDate)
        {
            if (endDate <= startDate)
                return Result.Failure(ProjectErrors.InvalidEndDate);

            if (maxTaskDueDate.HasValue && maxTaskDueDate.Value > endDate)
                return Result.Failure(ProjectErrors.EndDateBeforeTaskDueDate);

            return Result.Success();
        }

        public Result ChangeManager(Guid newManagerId, bool hasStartedTasks)
        {
            var notCompletedResult = EnsureNotCompleted();
            if (notCompletedResult.IsFailure)
                return notCompletedResult;

            if (hasStartedTasks)
                return Result.Failure(ProjectErrors.CannotChangeManager);

            ManagerId = newManagerId;
            return Result.Success();
        }

        public Result EnsureCanAddTask(DateTimeOffset taskStartDate, DateTimeOffset taskDueDate)
        {
            if (Status is ProjectStatus.Completed or ProjectStatus.Archived)
                return Result.Failure(ProjectErrors.CannotAddTask);

            if (taskStartDate < StartDate)
                return Result.Failure(ProjectErrors.TaskStartBeforeProject);

            if (taskDueDate > EndDate)
                return Result.Failure(ProjectErrors.TaskDueAfterProject);

            if (taskDueDate <= taskStartDate)
                return Result.Failure(ProjectErrors.InvalidTaskDates);

            return Result.Success();
        }

        public Result EnsureCanModifyTasks()
        {
            if (Status is ProjectStatus.Completed or ProjectStatus.Archived)
                return Result.Failure(ProjectErrors.CannotModifyCompleted);

            return Result.Success();
        }

        // ---------- Status Transitions ----------

        public Result Activate()
        {
            if (Status != ProjectStatus.Planning)
                return Result.Failure(ProjectErrors.InvalidActivation);

            Status = ProjectStatus.Active;
            return Result.Success();
        }

        /// <summary>hasIncompleteTasks is resolved by the caller via an EXISTS query.</summary>
        public Result Complete(bool hasIncompleteTasks)
        {
            if (Status != ProjectStatus.Active)
                return Result.Failure(ProjectErrors.InvalidCompletion);

            if (hasIncompleteTasks)
                return Result.Failure(ProjectErrors.HasIncompleteTasks);

            Status = ProjectStatus.Completed;
            return Result.Success();
        }

        public Result Archive()
        {
            if (Status != ProjectStatus.Completed)
                return Result.Failure(ProjectErrors.InvalidArchive);

            Status = ProjectStatus.Archived;
            return Result.Success();
        }

        public Result CanBeDeleted(bool hasTasks) =>
            hasTasks ? Result.Failure(ProjectErrors.HasTasks) : Result.Success();

        private Result EnsureNotCompleted()
        {
            if (Status is ProjectStatus.Completed or ProjectStatus.Archived)
                return Result.Failure(ProjectErrors.CannotModifyCompleted);

            return Result.Success();
        }
    }
}