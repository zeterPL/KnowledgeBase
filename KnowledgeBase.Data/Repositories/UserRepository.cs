using KnowledgeBase.Data.Data;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IGenericRepository<User>, IUserRepository
    {
        public UserRepository(KnowledgeDbContext context) : base(context) { }

        public void AddPermissionsByUserIdAndRoleId(Guid userId, Guid roleId)
        {
            var rolePermissions = _context.Set<RolePermission>().Where(rp => rp.RoleId == roleId);
            var user = _context.Users.Where(user => user.Id == userId).FirstOrDefault();
            foreach (var permission in rolePermissions)
            {
                var currentPermission = _context.Set<Permission>().Select(p => p.Id == permission.PermissionId);
                var perm = new Permission
                {
                    PermissionName = permission.Permission.PermissionName,
                    UserId = userId,
                }; 
                _context.Set<Permission>().Add(perm);
                _context.SaveChanges();
            }
        }

        public IList<Permission>? GetAllUserPermissionsByUserId(Guid userId)
        {
            return _context.Set<Permission>().Where(x => x.UserId == userId).ToList<Permission>();
        }
    }
}
