using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskManagement.Domain.Common.Results;

namespace TaskManagement.Application.Features.Projects.Commands.CreateProject
{
    public sealed record CreateProjectCommand(
    string Name,
    string Description,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate,
    Guid ManagerId
) : IRequest<Result<Guid>>;
}
