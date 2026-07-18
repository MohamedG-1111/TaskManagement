using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Infrastructure.Identity;

namespace TaskManagement.Infrastructure.Persistence.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.ManagedProjects)
                .WithOne()
                .HasForeignKey(p => p.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.UserTasks)
                .WithOne()
                .HasForeignKey(pt => pt.AssignedUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
