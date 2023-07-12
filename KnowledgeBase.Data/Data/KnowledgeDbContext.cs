using KnowledgeBase.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.Data.Data;

public class KnowledgeDbContext : IdentityDbContext<User, Role, Guid>
{
	public KnowledgeDbContext()
	{ }

	public KnowledgeDbContext(DbContextOptions<KnowledgeDbContext> options) : base(options)
	{ }

	private DbSet<Project> Projects { get; set; }
	private DbSet<Resource> Resources { get; set; }
	private DbSet<User> Users { get; set; }
	private DbSet<Role> Roles { get; set; }
	private DbSet<UserProject> UserProjects { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<UserProject>().HasKey(up => new { up.ProjectId, up.UserId });

		builder.Entity<User>().HasMany(e => e.Resources)
			.WithOne(e => e.User).HasForeignKey(e => e.UserId)
			.IsRequired(false);

		builder.Entity<Project>().HasMany(e => e.Resources)
			.WithOne(e => e.Project).HasForeignKey(e => e.ProjectId)
			.IsRequired(false);
	}
}