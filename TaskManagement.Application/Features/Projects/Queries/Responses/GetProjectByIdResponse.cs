using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Features.Projects.Queries.Responses
{
    public sealed record GetProjectByIdResponse(
    Guid Id,
    string Name,
    string Description,
    ProjectStatus Status,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate);
}
