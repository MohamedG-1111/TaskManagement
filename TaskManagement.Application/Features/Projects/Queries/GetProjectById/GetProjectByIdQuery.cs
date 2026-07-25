using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskManagement.Domain.Common.Results;

namespace TaskManagement.Application.Features.Projects.Queries.GetProjectById
{
    public sealed record GetProjectByIdQuery(Guid Id)
        : IRequest<Result<GetProjectByIdResponse>>;
}
