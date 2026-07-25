using FluentValidation;
namespace TaskManagement.Application.Features.Projects.Queries.GetProjectById;

public sealed class GetProjectByIdQueryValidator
    : AbstractValidator<GetProjectByIdQuery>
{
    public GetProjectByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Project Id is required.");
    }
}