using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskManagement.Domain.Common.Results;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Errors;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateProjectCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await unitOfWork.ProjectRepository.GetByIdAsync(request.Id);

            if (project is null)
                return Result.Failure(ProjectErrors.NotFound);

            var isRepeatedName = await unitOfWork.ProjectRepository
                .ExistsByNameExceptAsync(request.Id, request.Name, cancellationToken);

            if (isRepeatedName)
                return Result.Failure(ProjectErrors.NameAlreadyExists);

            var maxTaskDueDate = await unitOfWork.ProjectRepository
                .GetMaxTaskDueDateAsync(request.Id, cancellationToken);

            var result = project.UpdateDetails(
                request.Name,
                request.Description,
                request.StartDate,
                request.EndDate,
                maxTaskDueDate);

            if (result.IsFailure)
                return result;

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
