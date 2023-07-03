using KnowledgeBase.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace KnowledgeBase.Data.Data
{
    public class DbContext : IdentityDbContext<User>
    {
        public DbContext(DbContextOptions<DbContext> options) : base(options) 
        {
                
        }

        DbSet<Project> Projects { get; set; }
        DbSet<Resource> Resources { get; set; }
        DbSet<User> Users { get; set; }

    }
}
