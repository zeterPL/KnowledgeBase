using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Data.Repositories.Interfaces;

namespace KnowledgeBase.Data.Repositories
{
    public class UserResourcePermissionRepository : GenericRepository<UserResourcePermission>,
        IGenericRepository<UserResourcePermission>, IUserResourcePermissionRepository
    {
        public UserResourcePermissionRepository(KnowledgeDbContext context) : base(context)
        {
        }

        public void AddRange(IList<UserResourcePermission> resourcePermissions)
        {
            _context.Set<UserResourcePermission>().AddRange(resourcePermissions);
        }

        public bool CheckIfUserHasPermissionsToResource(Guid resourceId, Guid userId, ResourcePermissionName permissionName)
        {
            bool check = _context.Set<UserResourcePermission>().Any(p => p.UserId == userId && p.ResourceId == resourceId && p.Name == permissionName);
            return check;
        }
    }
}