using KnowledgeBase.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public IList<Permission>? GetAllUserPermissionsByUserId(Guid userId);
        public void AddPermissionsByUserIdAndRoleId(Guid userId, Guid roleId);
        
    }
}
