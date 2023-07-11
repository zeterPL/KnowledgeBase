using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IGenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(KnowledgeDbContext context) : base(context) {   }
    }
}
