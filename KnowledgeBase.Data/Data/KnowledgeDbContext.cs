using KnowledgeBase.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.Data.Data;

public class KnowledgeDbContext : IdentityDbContext<User, Role, Guid>
{
    public KnowledgeDbContext()
    {
    }

    public KnowledgeDbContext(DbContextOptions<KnowledgeDbContext> options) : base(options)
    {

    }

    DbSet<Project> Projects { get; set; }
    DbSet<Resource> Resources { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<UserProject> UserProjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
            "");
        }

    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /*
        if(!Roles.Any())
        {
            builder.Entity<Role>().HasData(new Role()
            {
                Id = Guid.NewGuid(),
                Name = KnowledgeBase.Data.Models.UserRoles.SuperAdmin.ToString(),
                Description = "SuperAdmin role"
            },
            new Role()
            {
                Id = Guid.NewGuid(),
                Name = KnowledgeBase.Data.Models.UserRoles.Admin.ToString(),
                Description = "Admin role"
            },
            new Role()
            {
                Id = Guid.NewGuid(),
                Name = KnowledgeBase.Data.Models.UserRoles.Basic.ToString(),
                Description = "Basic role"
            });
        }
        */
        



        builder.Entity<UserProject>().HasKey(up => new { up.ProjectId, up.UserId});

        builder.Entity<User>().HasMany(e => e.Resources)
            .WithOne(e => e.User).HasForeignKey(e => e.UserId)
            .IsRequired(false);

        builder.Entity<Project>().HasMany(e => e.Resources)
            .WithOne(e => e.Project).HasForeignKey(e => e.ProjectId)
            .IsRequired(false);

    }
}
