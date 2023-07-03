namespace KnowledgeBase.Data.Models;

public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Resource> Resources { get; set; }
    public ICollection<User> User { get; set; }
}

