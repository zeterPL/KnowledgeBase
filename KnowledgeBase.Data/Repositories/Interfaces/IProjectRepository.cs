using KnowledgeBase.Data.Models;

namespace KnowledgeBase.Data.Repositories.Interfaces;

public interface IProjectRepository : IGenericRepository<Project>
{
    public void SoftDelete(Project project);

    public IEnumerable<Project> GetAllReadableByUser(Guid userId);

    public bool ProjectExists(Guid id);

    public bool ProjectExists(string name);

    public Task AddRangeAsync(IEnumerable<Project> projects);
}