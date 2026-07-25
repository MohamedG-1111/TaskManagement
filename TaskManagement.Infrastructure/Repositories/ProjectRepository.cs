using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;
using TaskManagement.Infrastructure.Persistence.Context;
using TaskStatus = TaskManagement.Domain.Enums.TaskStatus;

namespace TaskManagement.Infrastructure.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly AppDbContext context;

        public ProjectRepository(AppDbContext context) : base(context)
        {
            this.context = context;
            Console.WriteLine($"/n/n ContextFromProjectRepository : {context.GetHashCode()}\n\n");

        }

        public async Task<bool> HasTasksAsync(Guid projectId, CancellationToken cancellationToken) =>
            await context.Tasks
                .AnyAsync(t => t.ProjectId == projectId, cancellationToken);

        public async Task<bool> IsExistingNameAsync(string name, CancellationToken cancellationToken)
        {
            return await context.Projects
                .AnyAsync(p => p.Name.ToLower() == name.ToLower(), cancellationToken);
        }

        public async Task<bool> ExistsByNameExceptAsync(Guid projectId, string name, CancellationToken cancellationToken)
        {
            return await context.Projects
                            .AnyAsync(p => p.Name.ToLower() == name.ToLower() && p.Id != projectId, cancellationToken);
        }

        public async Task<DateTimeOffset?> GetMaxTaskDueDateAsync(Guid projectId, CancellationToken cancellationToken)
        {
            return await context.Tasks
                .Where(x => x.ProjectId == projectId)
                .MaxAsync(x => (DateTimeOffset?)x.DueDate, cancellationToken);
        }

        public async Task<bool> HasIncompleteTasks(Guid projectId, CancellationToken cancellationToken)
        {
            return await context.Tasks
                .AnyAsync(x => x.ProjectId == projectId &&
                ((x.Status == TaskStatus.InProgress) || x.Status == TaskStatus.Pending), cancellationToken);

        }
    }
}
