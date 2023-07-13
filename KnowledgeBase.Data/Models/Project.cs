using KnowledgeBase.Data.Models.Interfaces;

namespace KnowledgeBase.Data.Models;

public class Project : IDeletableEntity
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public virtual ICollection<Resource> Resources { get; set; }
	public bool IsDeleted { get; set; }
	public virtual ICollection<UserProjectPermission> UsersPermissions { get; set; }
}