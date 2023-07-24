using KnowledgeBase.Data.Models.Interfaces;

namespace KnowledgeBase.Data.Models;

public class Project : IDeletableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public virtual ICollection<Resource> Resources { get; set; }
    public bool IsDeleted { get; set; }
    public virtual ICollection<UserProjectPermission> UsersPermissions { get; set; }
    public virtual ICollection<ProjectTag> ProjectTags { get; set; }
    public virtual ICollection<ProjectInterestedUser> InterestedUsers { get; set; }
}