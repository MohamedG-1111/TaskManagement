using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Persistence.Configuration
{
    public class TaskConfiguration : IEntityTypeConfiguration<ProjectTask>
    {
        public void Configure(EntityTypeBuilder<ProjectTask> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(t => t.Description)
                   .HasMaxLength(1000);

            builder.Property(t => t.Priority)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(t => t.Status)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(t => t.DueDate)
                   .IsRequired();

            builder.Property(t => t.AssignedUserId)
                   .IsRequired();

            builder.HasOne(t => t.Project)
                   .WithMany()
                   .HasForeignKey(t => t.ProjectId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
