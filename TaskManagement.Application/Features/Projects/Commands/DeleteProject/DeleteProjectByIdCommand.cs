using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskManagement.Domain.Common.Results;

namespace TaskManagement.Application.Features.Projects.Commands.DeleteProject
{
    public record DeleteProjectByIdCommand(Guid Id) : IRequest<Result>;
}
