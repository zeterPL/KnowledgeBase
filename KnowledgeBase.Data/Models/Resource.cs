using KnowledgeBase.Data.Models.Interfaces;
using KnowledgeBase.Data.Models.Enums;


namespace KnowledgeBase.Data.Models;

public class Resource : IDeletableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ResourceCategory Category { get; set; }

    public Guid ProjectId { get; set; }
    public virtual Project? Project { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}