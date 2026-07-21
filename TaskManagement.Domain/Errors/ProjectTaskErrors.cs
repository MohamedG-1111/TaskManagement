using TaskManagement.Domain.Common.Results;

namespace TaskManagement.Domain.Errors;

public static class ProjectTaskErrors
{
    public static readonly Error NotFound =
        Error.NotFound(
            "ProjectTask.NotFound",
            "Task was not found.");

    public static readonly Error TitleRequired =
        Error.Validation(
            "ProjectTask.TitleRequired",
            "Task title is required.");

    public static readonly Error TitleTooLong =
        Error.Validation(
            "ProjectTask.TitleTooLong",
            "Task title cannot exceed 100 characters.");

    public static readonly Error DescriptionTooLong =
        Error.Validation(
            "ProjectTask.DescriptionTooLong",
            "Task description cannot exceed 500 characters.");

    public static readonly Error ProjectRequired =
        Error.Validation(
            "ProjectTask.ProjectRequired",
            "Project is required.");

    public static readonly Error AssignedUserRequired =
        Error.Validation(
            "ProjectTask.AssignedUserRequired",
            "Assigned user is required.");

    public static readonly Error StartDateRequired =
        Error.Validation(
            "ProjectTask.StartDateRequired",
            "Start date is required.");

    public static readonly Error DueDateRequired =
        Error.Validation(
            "ProjectTask.DueDateRequired",
            "Due date is required.");

    public static readonly Error StartDateInPast =
        Error.Validation(
            "ProjectTask.StartDateInPast",
            "Start date cannot be in the past.");

    public static readonly Error InvalidDueDate =
        Error.Validation(
            "ProjectTask.InvalidDueDate",
            "Due date must be after start date.");

    public static readonly Error CannotModifyCompleted =
        Error.Conflict(
            "ProjectTask.CannotModifyCompleted",
            "Completed tasks cannot be modified.");

    public static readonly Error CannotDeleteCompleted =
        Error.Conflict(
            "ProjectTask.CannotDeleteCompleted",
            "Completed tasks cannot be deleted.");

    public static readonly Error CannotReopenCancelled =
        Error.Conflict(
            "ProjectTask.CannotReopenCancelled",
            "Cancelled tasks cannot be reopened.");

    public static readonly Error InvalidReopen =
        Error.Conflict(
            "ProjectTask.InvalidReopen",
            "Only in-progress tasks can be reopened to pending.");

    public static readonly Error InvalidStart =
        Error.Conflict(
            "ProjectTask.InvalidStart",
            "Only pending tasks can be started.");

    public static readonly Error InvalidCompletion =
        Error.Conflict(
            "ProjectTask.InvalidCompletion",
            "Only tasks in progress can be completed.");

    public static readonly Error CannotCancelCompleted =
        Error.Conflict(
            "ProjectTask.CannotCancelCompleted",
            "Completed tasks cannot be cancelled.");

    public static readonly Error InvalidMoveStatus =
        Error.Conflict(
            "ProjectTask.InvalidMoveStatus",
            "Only pending tasks can be moved to another project.");
}