using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Models
{
    public class Right
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserRight> AssignedUsers { get; set; }
        public ICollection<UserRoleRight> AssignedUserRoleRights { get; set; }
    }
}
