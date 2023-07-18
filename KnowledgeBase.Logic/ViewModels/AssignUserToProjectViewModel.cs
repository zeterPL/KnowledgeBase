using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic.ViewModels
{
    public class AssignUserToProjectViewModel
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        public bool isAssigned { get; set; }
        public bool CanReadProject { get; set; }
        public bool CanEditProject { get; set; }
        public bool CanDeleteProject { get; set; }
        public bool HaveAllPermissions { get; set; }
    }
}
