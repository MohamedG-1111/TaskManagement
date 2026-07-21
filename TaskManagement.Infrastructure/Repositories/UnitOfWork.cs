using System.Collections.Concurrent;
using TaskManagement.Domain.Repositories;
using TaskManagement.Infrastructure.Persistence.Context;

namespace TaskManagement.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        private ConcurrentDictionary<Type, object> Repositories { get; set; }
           = new ConcurrentDictionary<Type, object>();

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }


        public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();

        public IRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);
            if (Repositories.TryGetValue(type, out var repo))
                return (IRepository<T>)repo;
            var newRepo = new Repository<T>(context);
            Repositories[type] = newRepo;
            return newRepo;
        }
    }
}
