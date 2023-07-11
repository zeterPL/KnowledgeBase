using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;

namespace KnowledgeBase.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IGenericRepository<User>, IUserRepository
    {
        public UserRepository(KnowledgeDbContext context) : base(context)
        {
        }

        public void AddPermissionsByUserIdAndRoleId(Guid userId, Guid roleId)
        {
        }

        public IList<UserProjectPermission>? GetAllUserPermissionsByUserId(Guid userId)
        {
            return _context.Set<UserProjectPermission>().Where(x => x.UserId == userId).ToList<UserProjectPermission>();
        }
    }
}