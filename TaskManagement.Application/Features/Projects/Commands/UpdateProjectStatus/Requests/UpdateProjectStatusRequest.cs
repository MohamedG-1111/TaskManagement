using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Features.Projects.Commands.UpdateProjectStatus.Requests
{
    public record UpdateProjectStatusRequest(
    ProjectStatus Status);
}
