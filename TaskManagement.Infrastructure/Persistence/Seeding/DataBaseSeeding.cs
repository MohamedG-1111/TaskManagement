using TaskManagement.Infrastructure.Persistence.Context;

namespace TaskManagement.Infrastructure.Persistence.Seeding
{
    public class DataBaseSeeding(AppDbContext Context, IEnumerable<IDataSeeder> Seeders)
    {
        public async Task SeedAllAsync(CancellationToken ct = default)
        {
            foreach (var seeder in Seeders.OrderBy(x => x.Order))
            {
                await seeder.SeedAsync(ct);
                await Context.SaveChangesAsync();

            }
        }
    }
}
