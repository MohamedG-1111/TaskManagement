using TaskManagement.Domain.Enums;

namespace TaskManagement.Infrastructure.Persistence.Seeding.Models
{
    public record ProjectSeedingModel
    (
         string Name,
         string Description,
         DateTimeOffset StartDate,
         DateTimeOffset EndDate,
         ProjectStatus Status,
         Guid ManagerId
    );
}
