using KnowledgeBase.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgeBase.Data.Configurations;

public class ProjectRequestConfiguration : IEntityTypeConfiguration<ProjectPermissionRequest>
{
    public void Configure(EntityTypeBuilder<ProjectPermissionRequest> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasOne(r => r.Sender)
            .WithMany(u => u.ProjectPermissionRequestsSended)
            .HasForeignKey(r => r.SenderId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(r => r.Receiver)
            .WithMany(u => u.ProjectPermissionRequestsReceived)
            .HasForeignKey(r => r.ReceiverId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(r => r.Project)
            .WithMany(u => u.ProjectPermissionRequests)
            .HasForeignKey(r => r.ProjectId);

        builder.Property(r => r.RequestedPermission)
            .HasConversion<string>();

        builder.Property(r => r.TimeRequested)
            .HasPrecision(0);
    }
}