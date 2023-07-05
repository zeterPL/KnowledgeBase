using KnowledgeBase.Data.Models;

namespace KnowledgeBase.Logic.Services;

public interface IProjectService
{
    public Project Add(Project project);
    public Project Update(Project project);
    public void Remove(Project project);
    public void SoftDelete(Project project);
    public IEnumerable<Project> GetAll();
    public Project Get(Guid id);
}
