using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgeBase.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
	public void Configure(EntityTypeBuilder<Role> builder)
	{
		builder.HasKey(r => r.Id);

		builder.Property(r => r.Description)
			.HasMaxLength(50);

		var roles = new Role[]
		{
			Create(1.ToGuid(), UserRoles.Basic.ToString(), "Basic user role"),
			Create(2.ToGuid(), UserRoles.Admin.ToString(), "Admin user role"),
			Create(3.ToGuid(), UserRoles.SuperAdmin.ToString(), "SuperAdmin user role"),
		};

		builder.HasData(roles);
	}

	private static Role Create(Guid id, string name, string description)
	{
		return new Role
		{
			Id = id,
			Name = name,
			Description = description,
		};
	}
}