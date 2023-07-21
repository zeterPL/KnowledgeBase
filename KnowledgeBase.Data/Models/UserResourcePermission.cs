using KnowledgeBase.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Models
{
    public class UserResourcePermission
    {
        public Guid Id { get; set; }
        public ResourcePermissionName Name { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid ResourceId { get; set; } 
        public virtual Resource Resource { get; set; }
    }
}
