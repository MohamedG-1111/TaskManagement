using TaskManagement.Domain.Common.Baseentity;
using TaskManagement.Domain.Common.Results;
using TaskManagement.Domain.Enums;
using TaskManagement.Domain.Errors;
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
            Title = title;
            Description = description;
            Priority = priority;
            StartDate = startDate;
            DueDate = dueDate;
            ProjectId = projectId;
            AssignedUserId = assignedUserId;
            Status = TaskStatus.Pending;
        }

        public static Result<ProjectTask> Create(
            string title,
            string? description,
            Priority priority,
            DateTimeOffset startDate,
            DateTimeOffset dueDate,
            Guid projectId,
            Guid assignedUserId)
        {
            var titleResult = ValidateTitle(title);
            if (titleResult.IsFailure)
                return Result<ProjectTask>.Failure(titleResult.Error);

            var descriptionResult = ValidateDescription(description);
            if (descriptionResult.IsFailure)
                return Result<ProjectTask>.Failure(descriptionResult.Error);

            var datesResult = ValidateDates(startDate, dueDate);
            if (datesResult.IsFailure)
                return Result<ProjectTask>.Failure(datesResult.Error);

            var projectIdResult = ValidateProjectId(projectId);
            if (projectIdResult.IsFailure)
                return Result<ProjectTask>.Failure(projectIdResult.Error);

            var assignedUserResult = ValidateAssignedUserId(assignedUserId);
            if (assignedUserResult.IsFailure)
                return Result<ProjectTask>.Failure(assignedUserResult.Error);

            var task = new ProjectTask(
                title,
                description,
                priority,
                startDate,
                dueDate,
                projectId,
                assignedUserId);

            return Result<ProjectTask>.Success(task);
        }

        public Result UpdateDetails(
            string title,
            string? description,
            Priority priority,
            DateTimeOffset startDate,
            DateTimeOffset dueDate)
        {
            var notCompletedResult = EnsureNotCompleted();
            if (notCompletedResult.IsFailure)
                return notCompletedResult;

            var titleResult = ValidateTitle(title);
            if (titleResult.IsFailure)
                return titleResult;

            var descriptionResult = ValidateDescription(description);
            if (descriptionResult.IsFailure)
                return descriptionResult;

            var datesResult = ValidateDates(startDate, dueDate);
            if (datesResult.IsFailure)
                return datesResult;

            Title = title;
            Description = string.IsNullOrWhiteSpace(description) ? null : description;
            StartDate = startDate;
            DueDate = dueDate;
            Priority = priority;

            return Result.Success();
        }

        private static Result ValidateProjectId(Guid projectId)
        {
            if (projectId == Guid.Empty)
                return Result.Failure(ProjectTaskErrors.ProjectRequired);

            return Result.Success();
        }

        private static Result ValidateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return Result.Failure(ProjectTaskErrors.TitleRequired);

            if (title.Length > TitleMaxLength)
                return Result.Failure(ProjectTaskErrors.TitleTooLong);

            return Result.Success();
        }

        private static Result ValidateDescription(string? description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return Result.Success();

            if (description.Length > DescriptionMaxLength)
                return Result.Failure(ProjectTaskErrors.DescriptionTooLong);

            return Result.Success();
        }

        private static Result ValidateDates(DateTimeOffset startDate, DateTimeOffset dueDate)
        {
            if (startDate == default)
                return Result.Failure(ProjectTaskErrors.StartDateRequired);

            if (dueDate == default)
                return Result.Failure(ProjectTaskErrors.DueDateRequired);

            if (startDate < DateTimeOffset.UtcNow)
                return Result.Failure(ProjectTaskErrors.StartDateInPast);

            if (dueDate <= startDate)
                return Result.Failure(ProjectTaskErrors.InvalidDueDate);

            return Result.Success();
        }

        public Result AssignTo(Guid userId)
        {
            var notCompletedResult = EnsureNotCompleted();
            if (notCompletedResult.IsFailure)
                return notCompletedResult;

            var assignedUserResult = ValidateAssignedUserId(userId);
            if (assignedUserResult.IsFailure)
                return assignedUserResult;

            AssignedUserId = userId;
            return Result.Success();
        }

        public Result Reopen()
        {
            if (Status == TaskStatus.Cancelled)
                return Result.Failure(ProjectTaskErrors.CannotReopenCancelled);

            if (Status != TaskStatus.InProgress)
                return Result.Failure(ProjectTaskErrors.InvalidReopen);

            Status = TaskStatus.Pending;
            return Result.Success();
        }

        private static Result ValidateAssignedUserId(Guid userId)
        {
            if (userId == Guid.Empty)
                return Result.Failure(ProjectTaskErrors.AssignedUserRequired);

            return Result.Success();
        }

        public Result Start()
        {
            if (Status != TaskStatus.Pending)
                return Result.Failure(ProjectTaskErrors.InvalidStart);

            Status = TaskStatus.InProgress;
            return Result.Success();
        }

        public Result Complete()
        {
            if (Status != TaskStatus.InProgress)
                return Result.Failure(ProjectTaskErrors.InvalidCompletion);

            Status = TaskStatus.Completed;
            return Result.Success();
        }

        public Result Cancel()
        {
            if (Status == TaskStatus.Completed)
                return Result.Failure(ProjectTaskErrors.CannotCancelCompleted);

            Status = TaskStatus.Cancelled;
            return Result.Success();
        }

        private Result EnsureNotCompleted()
        {
            if (Status == TaskStatus.Completed)
                return Result.Failure(ProjectTaskErrors.CannotModifyCompleted);

            return Result.Success();
        }

        public Result EnsureCanDelete()
        {
            if (Status == TaskStatus.Completed)
                return Result.Failure(ProjectTaskErrors.CannotDeleteCompleted);

            return Result.Success();
        }

        public Result ChangePriority(Priority priority)
        {
            var notCompletedResult = EnsureNotCompleted();
            if (notCompletedResult.IsFailure)
                return notCompletedResult;

            Priority = priority;
            return Result.Success();
        }

        public Result MoveToProject(Guid projectId)
        {
            if (Status != TaskStatus.Pending)
                return Result.Failure(ProjectTaskErrors.InvalidMoveStatus);

            var projectIdResult = ValidateProjectId(projectId);
            if (projectIdResult.IsFailure)
                return projectIdResult;

            ProjectId = projectId;
            return Result.Success();
        }
    }
}