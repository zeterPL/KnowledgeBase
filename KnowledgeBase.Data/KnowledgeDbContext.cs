using KnowledgeBase.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.Data;

public class KnowledgeDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
	public KnowledgeDbContext(DbContextOptions<KnowledgeDbContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		builder.ApplyConfigurationsFromAssembly(typeof(KnowledgeDbContext).Assembly);
	}
}