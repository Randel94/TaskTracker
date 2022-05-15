using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTracker.Models;

namespace TaskTracker.EntityConfiguration
{
    public class TaskEntityTypeConfiguration : IEntityTypeConfiguration<TaskModel>
    {
        public void Configure(EntityTypeBuilder<TaskModel> builder)
        {
            builder.HasKey(x => x.TaskId);

            builder.Property(x => x.Name)
                .IsRequired()
                .IsUnicode(false);

            builder.Property(x => x.Description)
                .IsRequired()
                .IsUnicode(false);

            builder.Property(x => x.Executor)
                .IsRequired()
                .IsUnicode(false);

            builder.Property(x => x.CompletedTime)
                .IsRequired(false);

            builder.HasOne(x => x.ParentTask)
                .WithMany()
                .HasForeignKey(x => x.ParentId);
        }
    }
}
