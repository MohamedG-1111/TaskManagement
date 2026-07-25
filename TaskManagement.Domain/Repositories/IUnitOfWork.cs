using TaskManagement.Domain.Common.Baseentity;

namespace TaskManagement.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : BaseEntity;

        Task<int> SaveChangesAsync();

    }
}
