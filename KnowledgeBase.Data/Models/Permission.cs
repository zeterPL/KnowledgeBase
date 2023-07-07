namespace KnowledgeBase.Data.Models;

public class Permission
{
    public string PermissionName { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public Guid ProjectId { get; set; }
    public virtual Project Project { get; set; }
}
