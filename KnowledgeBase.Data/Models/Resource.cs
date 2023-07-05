using KnowledgeBase.Data.Models.Interfaces;

namespace KnowledgeBase.Data.Models;

public class Resource : IDeletableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Project? Project { get; set; }
    public ResourceCategory Category { get; set; }
    public User? User { get; set; }
    
    public bool IsDeleted { get; set; }
}

