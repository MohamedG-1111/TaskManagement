using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Features.Projects.Queries.GetAllProjects.Filters
{
    public class ProjectFilter
    {
        public string? Search { get; init; }

        public ProjectStatus? Status { get; init; }

        public Guid? ManagerId { get; init; }

        public bool Descending { get; set; } = true;

        public DateTimeOffset? StartFrom { get; init; }
        public DateTimeOffset? StartTo { get; init; }

        public DateTimeOffset? EndFrom { get; init; }
        public DateTimeOffset? EndTo { get; init; }
    }
}
