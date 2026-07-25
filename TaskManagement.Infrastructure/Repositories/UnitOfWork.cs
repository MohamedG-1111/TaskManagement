using System.Collections.Concurrent;
using TaskManagement.Domain.Common.Baseentity;
using TaskManagement.Domain.Repositories;
using TaskManagement.Infrastructure.Persistence.Context;

namespace TaskManagement.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;



        public IProjectRepository ProjectRepository { get; }

        public UnitOfWork(AppDbContext context, IProjectRepository ProjectRepository)
        {
            this.context = context;
            this.ProjectRepository = ProjectRepository;

            Console.WriteLine($"/n/n ContextFromProjectUnitOfWork : {context.GetHashCode()}\n\n");
        }


        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => await context.SaveChangesAsync(cancellationToken);

    }
}
