using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskManagement.Domain.Common.Results;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Features.Projects.Commands.UpdateProjectStatus
{
    public record UpdateProjectStatusCommand(Guid Id, ProjectStatus Status) : IRequest<Result>;
}
