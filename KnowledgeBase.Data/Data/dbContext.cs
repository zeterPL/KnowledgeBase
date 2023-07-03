using KnowledgeBase.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.Data.Data
{
    public class dbContext : IdentityDbContext<User>
    {
        public dbContext() : base() 
        {
                
        }

        DbSet<Project> Projects { get; set; }
        DbSet<Resource> Resources { get; set; }
        DbSet<User> Users { get; set; }

    }
}
