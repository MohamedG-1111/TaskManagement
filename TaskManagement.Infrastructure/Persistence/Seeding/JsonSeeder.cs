using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Common.Baseentity;

namespace TaskManagement.Infrastructure.Persistence.Seeding
{
    public class JsonSeeder
    {
        private static readonly JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters =
    {
        new JsonStringEnumConverter()
    }
        };
        public static async Task SeedIfEmpty<TEntity, TModel>(DbSet<TEntity> dbset,
            string fileName, Func<TModel, TEntity> map
            , CancellationToken cancellationToken = default) where TEntity : BaseEntity
        {
            if (await dbset.AnyAsync())
                return;

            var filepath = Path.Combine(AppContext.BaseDirectory, "Persistence", "Seeding", "Data", fileName);
            if (!File.Exists(filepath))
                return;
            using var stream = File.OpenRead(filepath);

            var models = await JsonSerializer.DeserializeAsync<List<TModel>>(stream, options, cancellationToken);

            if (models is null || models.Count == 0)
                return;

            var entities = models.Select(map);

            await dbset.AddRangeAsync(entities, cancellationToken);

        }
    }
}
