using KnowledgeBase.Data.Data;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;

namespace KnowledgeBase.Data.Repositories;

public class PermissionRepository : GenericRepository<UserProjectPermission>, IPermissionRepository
{
    public PermissionRepository(KnowledgeDbContext context) : base(context)
    {
    }
}
