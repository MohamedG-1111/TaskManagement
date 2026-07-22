using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Common.Results;
namespace TaskManagement.Application.Extensions
{
    public static class PaginationExtensions
    {
        public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(
            this IQueryable<T> query,
            PaginationParameters parameters,
            CancellationToken ct = default)
        {
            var totalCount = await query.CountAsync(ct);

            var items = await query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync(ct);

            return PaginatedResult<T>.Create(items, totalCount, parameters);
        }
    }
}
