using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Models
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProjectTag> Projects { get; set; }
    }
}
