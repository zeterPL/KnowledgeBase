namespace KnowledgeBase.Data.Models;

public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Resource> Resources { get; set; }
    public virtual ICollection<UserProject> User { get; set; }
}

