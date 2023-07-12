using KnowledgeBase.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgeBase.Data.Configurations;

public class UserProjectPermissionConfiguration : IEntityTypeConfiguration<UserProjectPermission>
{
	public void Configure(EntityTypeBuilder<UserProjectPermission> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.PermissionName)
			.HasConversion<string>();

		builder.HasOne(x => x.User)
			.WithMany(u => u.ProjectsPermissions)
			.HasForeignKey(x => x.UserId);

		builder.HasOne(x => x.Project)
			.WithMany(p => p.UsersPermissions)
			.HasForeignKey(x => x.ProjectId);
	}
}