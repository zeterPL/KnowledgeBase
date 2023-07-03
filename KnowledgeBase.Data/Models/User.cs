namespace KnowledgeBase.Data.Models;

public class User
{
    public Guid Id { get; set; }
    public ICollection<Resource> Resources { get; set; }
    public ICollection<Project> Projects { get; set; }
}
