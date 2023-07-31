using KnowledgeBase.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic.Dto
{
    public class EmailMessage
    {
        public string SenderEmail { get; set; }
        public string RecipientEmail { get; set; }
        public string ProjectName { get; set; }
        public IEnumerable<ProjectPermissionName> Permissions { get; set; }
    }
}
