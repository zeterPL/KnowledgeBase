using KnowledgeBase.Data.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace KnowledgeBase.Data.Models;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }    
    
    public UserRoles AssignedRoleName { get; set; }
    
    public virtual ICollection<Resource> Resources { get; set; }
    public virtual ICollection<UserProject> AssignedProjects { get; set; }
    public virtual ICollection<Permission> ProjectsPermissions { get; set; }
}
