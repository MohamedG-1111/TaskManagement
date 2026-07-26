using TaskManagement.Domain.Common.Results;

namespace TaskManagement.Domain.Errors;

public static class ProjectErrors
{
    public static readonly Error NotFound =
        Error.NotFound(
            "Project.NotFound",
            "Project was not found.");

    public static readonly Error NameRequired =
        Error.Validation(
            "Project.NameRequired",
            "Project name is required.");

    public static readonly Error NameTooLong =
        Error.Validation(
            "Project.NameTooLong",
            "Project name cannot exceed 100 characters.");

    public static readonly Error DescriptionRequired =
        Error.Validation(
            "Project.DescriptionRequired",
            "Project description is required.");

    public static readonly Error DescriptionTooLong =
        Error.Validation(
            "Project.DescriptionTooLong",
            "Project description cannot exceed 500 characters.");

    public static readonly Error InvalidEndDate =
        Error.Validation(
            "Project.InvalidEndDate",
            "End date must be after the start date.");

    public static readonly Error EndDateBeforeTaskDueDate =
        Error.Validation(
            "Project.EndDateBeforeTaskDueDate",
            "Project end date cannot be earlier than any task due date.");

    public static readonly Error CannotModifyCompleted =
        Error.Conflict(
            "Project.CannotModifyCompleted",
            "Completed or archived projects cannot be modified.");

    public static readonly Error CannotChangeManager =
        Error.Conflict(
            "Project.CannotChangeManager",
            "Cannot change manager after a task has started.");

    public static readonly Error CannotAddTask =
        Error.Conflict(
            "Project.CannotAddTask",
            "Cannot add tasks to a completed or archived project.");

    public static readonly Error TaskStartBeforeProject =
        Error.Validation(
            "Project.TaskStartBeforeProject",
            "Task start date cannot be before project start date.");

    public static readonly Error TaskDueAfterProject =
        Error.Validation(
            "Project.TaskDueAfterProject",
            "Task due date cannot be after project end date.");

    public static readonly Error InvalidTaskDates =
        Error.Validation(
            "Project.InvalidTaskDates",
            "Task due date must be after task start date.");

    public static readonly Error InvalidActivation =
        Error.Conflict(
            "Project.InvalidActivation",
            "Only planning projects can be activated.");

    public static readonly Error InvalidCompletion =
        Error.Conflict(
            "Project.InvalidCompletion",
            "Only active projects can be completed.");

    public static readonly Error HasIncompleteTasks =
        Error.Conflict(
            "Project.HasIncompleteTasks",
            "Project still contains incomplete tasks.");

    public static readonly Error InvalidArchive =
        Error.Conflict(
            "Project.InvalidArchive",
            "Only completed projects can be archived.");

    public static readonly Error HasTasks =
        Error.Conflict(
            "Project.HasTasks",
            "Cannot delete a project that still contains tasks.");
    public static readonly Error DeleteFailed =
      Error.Failure(
          "Project.DeleteFailed",
          "Failed to delete the project.");
    public static readonly Error NameAlreadyExists =
        Error.Conflict(
       "Project.NameAlreadyExists",
       "A project with the same name already exists.");
    public static readonly Error InvalidCancellation = Error.Conflict(
     "Project.InvalidCancellation",
    "Only projects with Planning or Active status can be cancelled.");

    public static readonly Error InvalidStatus = Error.Validation(
    "Project.InvalidStatus",
    "The specified project status is invalid.");

    public static readonly Error AlreadyPlanning =
    Error.Conflict(
        "Project.AlreadyPlanning",
        "The project is already in planning status.");

    public static readonly Error InvalidReopen =
    Error.Validation(
        "Project.InvalidReopen",
        "Only cancelled projects can be reopened.");
}
