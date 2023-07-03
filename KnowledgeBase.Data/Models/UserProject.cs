using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Models
{
    public class UserProject
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
