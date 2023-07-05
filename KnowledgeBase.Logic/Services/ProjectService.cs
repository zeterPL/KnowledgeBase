using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;

namespace KnowledgeBase.Logic.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository projectRepository;
    public ProjectService(IProjectRepository projectRepository)
    {
        this.projectRepository = projectRepository;
    }

    public Project Add(Project project)
    {
        projectRepository.Add(project);
        return projectRepository.Get(project.Id);
    }

    public void Remove(Project project)
    {
        projectRepository.Remove(project);
    }

    public Project Get(Guid id)
    {
        return projectRepository.Get(id);
    }

    public IEnumerable<Project> GetAll()
    {
        return projectRepository.GetAll();
    }

    public Project Update(Project project)
    {
        projectRepository.Update(project);
        return projectRepository.Get(project.Id);
    }
}
