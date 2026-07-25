using FluentValidation;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Features.Projects.Commands.UpdateProjectStatus;

public sealed class UpdateProjectStatusCommandValidator : AbstractValidator<UpdateProjectStatusCommand>
{
    public UpdateProjectStatusCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Status)
            .IsInEnum();
    }
}