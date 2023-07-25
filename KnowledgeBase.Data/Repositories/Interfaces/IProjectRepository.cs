using KnowledgeBase.Data.Models;

namespace KnowledgeBase.Data.Repositories.Interfaces;

public interface IProjectRepository : IGenericRepository<Project>
{
    public void SoftDelete(Project project);

    public IEnumerable<Project> GetAllReadableByUser(Guid userId);

    public bool ProjectExists(Guid id);

    public bool ProjectExists(string name);

    /// <returns>Projects that exist in database</returns>
    public IEnumerable<Project> ProjectsExists(IEnumerable<string> names);

    public Task AddRangeAsync(IEnumerable<Project> projects);

    public Task<Guid> GetProjectOwnerId(Guid projectId);
}