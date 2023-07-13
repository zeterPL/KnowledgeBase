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

		var defaultProject = new Project
		{
			Id = Guid.Parse("8f94efce-fa7a-47d8-98e6-08db7ede4d7b"),
			Name = "Deafult project",
			IsDeleted = false,
		};
		builder.HasData(defaultProject);
	}
}