using KnowledgeBase.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgeBase.Data.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
	public void Configure(EntityTypeBuilder<Project> builder)
	{
		builder.HasKey(p => p.Id);

		builder.Property(p => p.Name)
			.HasMaxLength(100);

		builder.HasMany(p => p.Resources)
			.WithOne(r => r.Project)
			.HasForeignKey(r => r.ProjectId);

		builder.Property(p => p.Description)
			.HasMaxLength(500);

		builder.Property(p => p.StartDate)
			.HasPrecision(3);

		builder.HasOne(p => p.Owner)
			.WithMany(p => p.ProjectsOwned)
			.HasForeignKey(p => p.OwnerId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}