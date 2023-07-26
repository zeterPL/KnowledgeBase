using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic.Dto.Project
{
    public class ProjectSearchFilter
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public List<int>? TagsName { get; set; }
    }
}
