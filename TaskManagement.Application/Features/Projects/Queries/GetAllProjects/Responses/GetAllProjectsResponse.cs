using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Features.Projects.Queries.GetAllProjects.Responses;

public record GetAllProjectsResponse(
    Guid Id,
    string Name,
    ProjectStatus Status,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate);