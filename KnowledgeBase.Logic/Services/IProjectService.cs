using KnowledgeBase.Logic.Dto;

namespace KnowledgeBase.Logic.Services;

public interface IProjectService
{
    public Guid Add(ProjectDto project);
    public Guid Update(ProjectDto project);
    public void Remove(ProjectDto project);
    public void SoftDelete(ProjectDto project);
    public IEnumerable<ProjectDto> GetAll();
    public ProjectDto Get(Guid id);
}