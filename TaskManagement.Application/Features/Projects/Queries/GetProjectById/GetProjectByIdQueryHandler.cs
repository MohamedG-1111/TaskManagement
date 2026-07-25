using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Common.Results;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Errors;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.Projects.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Result<GetProjectByIdResponse>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetProjectByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<GetProjectByIdResponse>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await unitOfWork.ProjectRepository.GetAsQuery()
                .Where(x => x.Id == request.Id)
                .Select(x => new GetProjectByIdResponse
                (
                   x.Id,
                   x.Name,
                   x.Description,
                   x.Status,
                   x.StartDate,
                   x.EndDate
                )).FirstOrDefaultAsync();
            if (response is null)
                return Result<GetProjectByIdResponse>.Failure(ProjectErrors.NotFound);
            return response;
        }
    }
}