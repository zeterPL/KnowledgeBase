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

public class AzureResourceConfiguration : IEntityTypeConfiguration<AzureResource>
{
    public void Configure(EntityTypeBuilder<AzureResource> builder)
    {
        builder.Property(r => r.AzureFileName)
            .HasMaxLength(104);

        builder.Property(r => r.AzureStorageAbsolutePath)
            .HasMaxLength(225);
    }
}

public class CredentialResourceConfiguration : IEntityTypeConfiguration<CredentialsResource>
{
    public void Configure(EntityTypeBuilder<CredentialsResource> builder)
    {
        builder.Property(r => r.Login)
            .HasMaxLength(30);

        builder.Property(r => r.Password)
            .HasMaxLength(50);

        builder.Property(r => r.Target)
            .HasMaxLength(100);
    }
}

public class NoteResourceConfiguration : IEntityTypeConfiguration<NoteResource>
{
    public void Configure(EntityTypeBuilder<NoteResource> builder)
    {
        builder.Property(r => r.Note)
            .HasMaxLength(500);
    }
}
