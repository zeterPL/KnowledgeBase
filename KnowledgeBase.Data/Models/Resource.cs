namespace KnowledgeBase.Data.Models;

public class Resource
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Project Project { get; set; }
    public ResourceCategory Category { get; set; }
}

