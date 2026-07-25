using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskManagement.Application.Features.Projects.Queries.GetAllProjects.Filters;
using TaskManagement.Application.Features.Projects.Queries.GetAllProjects.Responses;
using TaskManagement.Domain.Common.Results;

namespace TaskManagement.Application.Features.Projects.Queries.GtAllProjects
{
    public record GetAllProjectsQuery(PaginationParameters PaginationParameters, ProjectFilter Filter)
        : IRequest<Result<PaginatedResult<GetAllProjectsResponse>>>;

}
