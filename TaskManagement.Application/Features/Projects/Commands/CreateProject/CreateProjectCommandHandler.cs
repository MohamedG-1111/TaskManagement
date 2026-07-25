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

namespace TaskManagement.Application.Features.Projects.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result<Guid>>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateProjectCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var IRepeatedName = await unitOfWork.ProjectRepository.IsExistingNameAsync(request.Name, cancellationToken);
            if (IRepeatedName)
                return Result<Guid>.Failure(ProjectErrors.NameAlreadyExists);

            var result = Project.Create(request.Name, request.Description, request.StartDate, request.EndDate, request.ManagerId);
            if (result.IsFailure)
                return Result<Guid>.Failure(result.Errors);

            await unitOfWork.ProjectRepository.AddAsync(result.Value);
            return await unitOfWork.SaveChangesAsync() > 0 ?
                  Result<Guid>.Success(result.Value.Id)
                  : Result<Guid>.Failure(Error.Failure("Project.Unexpected", "An unexpected error occurred."));
        }
    }
}
