using KnowledgeBase.Data.Models;

namespace KnowledgeBase.Data.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public IList<UserProjectPermission>? GetAllUserPermissionsByUserId(Guid userId);

        public void AddPermissionsByUserIdAndRoleId(Guid userId, Guid roleId);
    }
}