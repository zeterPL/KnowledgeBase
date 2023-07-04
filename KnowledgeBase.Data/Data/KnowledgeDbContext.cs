using KnowledgeBase.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.Data.Data;

public class KnowledgeDbContext : DbContext
{
    public KnowledgeDbContext()
    {
    }

    public KnowledgeDbContext(DbContextOptions<KnowledgeDbContext> options) : base(options)
    {
    }
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseSqlServer(
			"");
		}

	}

	

	DbSet<Project> Projects { get; set; }
    DbSet<Resource> Resources { get; set; }
    DbSet<User> Users { get; set; }
}
