using Microsoft.AspNetCore.Identity;

namespace KnowledgeBase.Data.Models;

public class User : IdentityUser<Guid>
{
    public virtual ICollection<Resource> Resources { get; set; }
    public virtual ICollection<UserProject> AssignedProjects { get; set; }
}
