using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Models
{
    public class UserRight
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid RightId { get; set; }
        public virtual Right Right { get; set; }
    }
}
