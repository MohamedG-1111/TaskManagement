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

namespace TaskManagement.Application.Features.Projects.Commands.DeleteProject
{
    public class DeleteProjectByIdCommandHandler : IRequestHandler<DeleteProjectByIdCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteProjectByIdCommandHandler(IUnitOfWork UnitOfWork)
        {
            unitOfWork = UnitOfWork;
        }
        public async Task<Result> Handle(DeleteProjectByIdCommand request, CancellationToken cancellationToken)
        {
            var project = await unitOfWork.ProjectRepository.GetByIdAsync(request.Id);
            if (project == null)
                return Result.Failure(ProjectErrors.NotFound);

            var HasTasks = await unitOfWork.ProjectRepository.HasTasksAsync(request.Id, cancellationToken);

            var result = project.CanBeDeleted(HasTasks);
            if (result.IsFailure)
                return result;
            unitOfWork.ProjectRepository.Delete(project);


            return await unitOfWork.SaveChangesAsync(cancellationToken) > 0
                ? Result.Success()
                : Result.Failure(ProjectErrors.DeleteFailed);
        }
    }
}
