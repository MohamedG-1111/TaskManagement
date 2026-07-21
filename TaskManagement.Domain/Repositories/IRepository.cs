namespace TaskManagement.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);


        Task<T?> GetByIdAsync(int id);
        Task<T?> FindAsync(int id);

        void Update(T entity);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entities);


        IQueryable<T> GetAsQuery(bool NoTracking = true);

        public Task<IEnumerable<T>> GetAllAsync();
    }
}
