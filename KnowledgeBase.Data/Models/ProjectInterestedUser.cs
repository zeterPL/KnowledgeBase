using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Models
{
    public class ProjectInterestedUser
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public Guid UserId { get; set; }    
        public User User { get; set; }
        public Guid ProjectId { get;set; }
        public Project Project { get; set; }
    }
}
