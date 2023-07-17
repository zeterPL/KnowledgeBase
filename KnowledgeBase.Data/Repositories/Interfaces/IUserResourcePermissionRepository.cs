using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Repositories.Interfaces
{
    public interface IUserResourcePermissionRepository : IGenericRepository<UserResourcePermission>
    {
        public bool CheckIfUserHasPermissionsToResource(Guid resourceId, Guid userId, ResourcePermissionName permissionName);   
    }
}
