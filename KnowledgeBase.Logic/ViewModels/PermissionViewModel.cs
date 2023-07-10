using KnowledgeBase.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic.ViewModels
{
    public class PermissionViewModel
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public List<PermissionDto> Permissions { get; set; }
    }
}
