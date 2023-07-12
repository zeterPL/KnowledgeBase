using KnowledgeBase.Data.Models.Enums;

namespace KnowledgeBase.Data.Models;

public class UserProjectPermission
{
	public Guid Id { get; set; }
	public ProjectPermissionName PermissionName { get; set; }

	public Guid UserId { get; set; }
	public virtual User User { get; set; }
	public Guid ProjectId { get; set; }
	public virtual Project Project { get; set; }
}