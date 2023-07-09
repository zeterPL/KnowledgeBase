using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;
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
            Create(UserRoles.Basic.ToString(), "Basic user role"),
            Create(UserRoles.Admin.ToString(), "Admin user role"),
            Create(UserRoles.SuperAdmin.ToString(), "SuperAdmin user role"),
        };

        builder.HasData(roles);
    }

    private static Role Create(string name, string description)
    {
        return new Role
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
        };
    }
}