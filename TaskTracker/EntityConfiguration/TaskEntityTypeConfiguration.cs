using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Models.Entities;

namespace TaskTracker.EntityConfiguration
{
    public class TaskEntityTypeConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.HasKey(x => x.TaskId);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("varchar(100)")
                .IsUnicode(false);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnType("varchar(100)")
                .IsUnicode(false);

            builder.Property(x => x.Executor)
                .IsRequired()
                .HasColumnType("varchar(100)")
                .IsUnicode(false);

            builder.Property(x => x.EstimatedTime)
                .IsRequired()
                .HasColumnType("decimal(6,2)");

            builder.Property(x => x.CompletedTime)
                .IsRequired(false)
                .HasColumnType("decimal(6,2)");

            builder.HasOne(x => x.ParentTask)
                .WithMany(x => x.ChildTasks)
                .HasForeignKey(x => x.ParentId);
        }
    }
}
