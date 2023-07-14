using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Data.Models.Interfaces;

namespace KnowledgeBase.Data.Models;

public class Resource : IDeletableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ResourceCategory Category { get; set; }
    public bool IsDeleted { get; set; }

    public Guid ProjectId { get; set; }
    public virtual Project? Project { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    public string AzureStorageAbsolutePath { get; set; }
    public string AzureFileName { get; set; }

    public virtual ICollection<UserResourcePermission> UserPermissions { get; set; }
}