using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<bool> HasTasksAsync(Guid projectId, CancellationToken cancellationToken);
        Task<bool> IsExistingNameAsync(string name, CancellationToken cancellationToken);
        Task<bool> ExistsByNameExceptAsync(Guid projectId, string name, CancellationToken cancellationToken);
        Task<bool> HasIncompleteTasks(Guid projectId, CancellationToken cancellationToken);

        public Task<DateTimeOffset?> GetMaxTaskDueDateAsync(Guid projectId, CancellationToken cancellationToken);

    }
}
