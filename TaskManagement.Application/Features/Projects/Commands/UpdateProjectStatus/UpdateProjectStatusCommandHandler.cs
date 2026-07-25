using MediatR;
using TaskManagement.Domain.Common.Results;
using TaskManagement.Domain.Enums;
using TaskManagement.Domain.Errors;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Features.Projects.Commands.UpdateProjectStatus;

public class UpdateProjectStatusCommandHandler
    : IRequestHandler<UpdateProjectStatusCommand, Result>
{
    private readonly IUnitOfWork unitOfWork;

    public UpdateProjectStatusCommandHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateProjectStatusCommand request, CancellationToken cancellationToken)
    {
        var project = await unitOfWork.ProjectRepository.GetByIdAsync(request.Id);

        if (project is null)
            return Result.Failure(ProjectErrors.NotFound);

        Result result = request.Status switch
        {
            ProjectStatus.Active =>
                project.Activate(),

            ProjectStatus.Completed =>
                project.Complete(
                    await unitOfWork.ProjectRepository
                        .HasIncompleteTasks(request.Id, cancellationToken)),

            ProjectStatus.Cancelled =>
                project.Cancel(),

            ProjectStatus.Archived =>
                project.Archive(),

            _ => Result.Failure(ProjectErrors.InvalidStatus)
        };

        if (result.IsFailure)
            return result;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}