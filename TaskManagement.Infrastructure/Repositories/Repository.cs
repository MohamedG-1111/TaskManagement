using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Common.Baseentity;
using TaskManagement.Domain.Repositories;
using TaskManagement.Infrastructure.Persistence.Context;

public class Repository<T> : IRepository<T>
    where T : BaseEntity
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task AddAsync(T entity)
        => await _dbSet.AddAsync(entity);

    public async Task AddRangeAsync(IEnumerable<T> entities)
        => await _dbSet.AddRangeAsync(entities);

    public async Task<T?> GetByIdAsync(int id)
        => await _dbSet.FindAsync(id);

    public async Task<T?> FindAsync(int id)
        => await _dbSet.FindAsync(id);

    public void Update(T entity)
        => _dbSet.Update(entity);

    public void Delete(T entity)
        => _dbSet.Remove(entity);

    public void DeleteRange(IEnumerable<T> entities)
        => _dbSet.RemoveRange(entities);

    public IQueryable<T> GetAsQuery(bool noTracking = true)
        => noTracking
            ? _dbSet.AsNoTracking()
            : _dbSet;

    public async Task<IEnumerable<T>> GetAllAsync()
        => await _dbSet.ToListAsync();
}