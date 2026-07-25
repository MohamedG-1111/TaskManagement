using TaskManagement.Domain.Common.Baseentity;

namespace TaskManagement.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IProjectRepository ProjectRepository { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
