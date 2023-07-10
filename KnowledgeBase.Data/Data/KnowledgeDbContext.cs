using KnowledgeBase.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.Data.Data;

public class KnowledgeDbContext : IdentityDbContext<User, Role, Guid>
{
    public KnowledgeDbContext() { }

    public KnowledgeDbContext(DbContextOptions<KnowledgeDbContext> options) : base(options)
    { }

    DbSet<Project> Projects { get; set; }
    DbSet<Resource> Resources { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<UserProject> UserProjects { get; set; }
    DbSet<Right> Rights { get; set; }
    DbSet<UserRoleRight> UserRoleRights { get; set; }
    DbSet<UserRole> UserRoles { get; set; }
    DbSet<UserRight> UserRights { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserProject>().HasKey(up => new { up.ProjectId, up.UserId});
        builder.Entity<UserRoleRight>().HasKey(urr => new { urr.UserId, urr.RoleId, urr.RightId });
        builder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
        builder.Entity<UserRight>().HasKey(ur => new { ur.UserId, ur.RightId });

        builder.Entity<User>().HasMany(e => e.Resources)
            .WithOne(e => e.User).HasForeignKey(e => e.UserId)
            .IsRequired(false);

        builder.Entity<Project>().HasMany(e => e.Resources)
            .WithOne(e => e.Project).HasForeignKey(e => e.ProjectId)
            .IsRequired(false);
    }
}
