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

    DbSet<Project> Projects { get; set; }
    DbSet<Resource> Resources { get; set; }
    DbSet<User> Users { get; set; }
}
