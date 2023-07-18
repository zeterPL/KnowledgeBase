using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic.Dto
{
    public class AddTagDto
    {
        public Guid ProjectId { get; set; }
        public string Tags { get; set; }
    }
}
