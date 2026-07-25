using MediatR;
using TaskManagement.Domain.Common.Results;

namespace TaskManagement.Application.Features.Projects.Commands.UpdateProject;

public sealed record UpdateProjectCommand(
    Guid Id,
    string Name,
    string Description,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate)
    : IRequest<Result>;