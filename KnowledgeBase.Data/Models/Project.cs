namespace KnowledgeBase.Data.Models;

public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public List<Resource> Resources { get; set; }

    public User User { get; set; }
}

