using FluentValidation;
using TaskManagement.Application.Common.Validators;
using TaskManagement.Application.Features.Projects.Queries.GtAllProjects;

namespace TaskManagement.Application.Features.Projects.Queries.GetAllProjects
{
    public class GetAllProjectQueryValidator
        : AbstractValidator<GetAllProjectsQuery>
    {
        public GetAllProjectQueryValidator()
        {
            RuleFor(x => x.PaginationParameters)
                .SetValidator(new PaginationParametersValidator());
        }
    }
}