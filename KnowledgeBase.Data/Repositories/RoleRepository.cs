using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;

namespace KnowledgeBase.Data.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IGenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(KnowledgeDbContext context) : base(context)
        {
        }
    }
}