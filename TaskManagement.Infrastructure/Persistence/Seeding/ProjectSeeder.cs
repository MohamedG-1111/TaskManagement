using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Persistence.Context;
using TaskManagement.Infrastructure.Persistence.Seeding.Models;

namespace TaskManagement.Infrastructure.Persistence.Seeding
{
    public class ProjectSeeder(AppDbContext AppContext) : IDataSeeder
    {
        public int Order => 1;
        public async Task SeedAsync(CancellationToken cancellationToken)
     => await JsonSeeder.SeedIfEmpty<Project, ProjectSeedingModel>(
         AppContext.Projects, "Project.json", p =>
         {
             var result = Project.Create(
                 p.Name,
                 p.Description,
                 p.StartDate,
                 p.EndDate,
                 p.ManagerId);

             if (result.IsFailure)
                 throw new InvalidOperationException(
                     $"Failed to seed project '{p.Name}': {string.Join(", ", result.Errors)}");

             return result.Value;
         }, cancellationToken);


    }
}
