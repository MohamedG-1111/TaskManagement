using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace TaskManagement.Application.Features.Projects.Commands.DeleteProject
{
    public class DeleteProjectByIdCommandValidator : AbstractValidator<DeleteProjectByIdCommand>
    {
        public DeleteProjectByIdCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
