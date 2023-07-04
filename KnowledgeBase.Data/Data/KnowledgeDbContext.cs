using KnowledgeBase.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.Data.Data;

public class KnowledgeDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
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
    DbSet<UserProject> UserProjects { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<UserProject>().HasKey(up => new { up.ProjectId, up.UserId});

        builder.Entity<User>().HasMany(e => e.Resources)
            .WithOne(e => e.User).HasForeignKey(e => e.UserId)
            .IsRequired(false);

        builder.Entity<Project>().HasMany(e => e.Resources)
            .WithOne(e => e.Project).HasForeignKey(e => e.ProjectId)
            .IsRequired(false);

    }
}
