using FluentValidation;
using TaskManagement.Domain.Common.Results;

namespace TaskManagement.Application.Common.Validators
{
    public class PaginationParametersValidator
        : AbstractValidator<PaginationParameters>
    {
        public PaginationParametersValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100);
        }
    }
}