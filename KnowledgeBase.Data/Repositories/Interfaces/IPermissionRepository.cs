using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;

namespace KnowledgeBase.Data.Repositories.Interfaces;

public interface IPermissionRepository : IGenericRepository<UserProjectPermission>
{
    public bool UserHasProjectPermission(Guid userId, Guid projectId, ProjectPermissionName permission);
}
