using KnowledgeBase.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgeBase.Data.Configurations;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .HasMaxLength(100);

        builder.Property(r=>r.AzureFileName)
            .HasMaxLength(104);

        builder.Property(r => r.AzureStorageAbsolutePath)
            .HasMaxLength(225);

        builder.Property(r => r.Description)
            .HasMaxLength(500);

        builder.HasOne(r => r.Project)
            .WithMany(p => p.Resources)
            .HasForeignKey(r => r.ProjectId)
            .IsRequired(false);

        builder.HasOne(r => r.User)
            .WithMany(u => u.Resources)
            .HasForeignKey(r => r.UserId)
            .IsRequired();

        builder.Property(r => r.Category)
            .HasConversion<string>();
    }
}