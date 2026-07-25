using MediatR;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Features.Projects.Queries.GetAllProjects.Responses;
using TaskManagement.Application.Features.Projects.Queries.GtAllProjects;
using TaskManagement.Domain.Common.Results;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.Projects.Queries.GetAllProjects
{
    public class GetAllProjectQueryHandler
        : IRequestHandler<GetAllProjectsQuery, Result<PaginatedResult<GetAllProjectsResponse>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAllProjectQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<PaginatedResult<GetAllProjectsResponse>>> Handle(
            GetAllProjectsQuery request,
            CancellationToken cancellationToken)
        {
            var query = unitOfWork.ProjectRepository
                .GetAsQuery();

            if (!string.IsNullOrWhiteSpace(request.Filter.Search))
            {
                query = query.Where(x =>
                    x.Name.Contains(request.Filter.Search) ||
                    x.Description.Contains(request.Filter.Search));
            }

            if (request.Filter.Status.HasValue)
            {
                query = query.Where(x =>
                    x.Status == request.Filter.Status);
            }

            if (request.Filter.ManagerId.HasValue)
            {
                query = query.Where(x =>
                    x.ManagerId == request.Filter.ManagerId);
            }


            query = request.Filter.Descending
                ? query.OrderByDescending(x => x.CreatedAt)
                : query.OrderBy(x => x.CreatedAt);

            var response = await query.Select(x => new GetAllProjectsResponse(
                    x.Id,
                    x.Name,
                    x.Status,
                    x.StartDate,
                    x.EndDate))
                .ToPaginatedListAsync(
                    request.PaginationParameters,
                    cancellationToken);

            return response;
        }
    }
}