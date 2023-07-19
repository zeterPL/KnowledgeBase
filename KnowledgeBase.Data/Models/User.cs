using KnowledgeBase.Data.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace KnowledgeBase.Data.Models;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid RoleId { get; set; }
    public virtual Role Role { get; set; }
    public virtual ICollection<Resource> Resources { get; set; }
    public virtual ICollection<UserProjectPermission> ProjectsPermissions { get; set; }
    public virtual ICollection<ProjectInterestedUser> ProjectInteresteds { get; set; }
}