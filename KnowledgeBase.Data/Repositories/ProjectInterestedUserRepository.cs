using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Repositories
{
    public class ProjectInterestedUserRepository : GenericRepository<ProjectInterestedUser>, 
        IGenericRepository<ProjectInterestedUser>, IProjectInterestedUserRepository
    {
        public ProjectInterestedUserRepository(KnowledgeDbContext context) : base(context) {  }

        public void AddRange(IList<ProjectInterestedUser> projectInterestedUsers)
        {
            _context.Set<ProjectInterestedUser>().AddRange(projectInterestedUsers);
            _context.SaveChanges();
        }
    }
}
