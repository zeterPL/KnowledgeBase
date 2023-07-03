using Microsoft.AspNetCore.Identity;

namespace KnowledgeBase.Data.Models;

public class User : IdentityUser
{
    //public Guid Id { get; set; }
    public virtual ICollection<Resource> Resources { get; set; }
    public virtual ICollection<UserProject> Projects { get; set; }
}
