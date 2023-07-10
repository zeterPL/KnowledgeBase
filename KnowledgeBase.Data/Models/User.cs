using KnowledgeBase.Data.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace KnowledgeBase.Data.Models;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserRoles AssignedRole { get; set; }
    public virtual ICollection<Resource> Resources { get; set; }
    public virtual ICollection<Project> AssignedProjects { get; set; }
}