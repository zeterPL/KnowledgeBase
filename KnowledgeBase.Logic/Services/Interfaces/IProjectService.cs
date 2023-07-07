using KnowledgeBase.Logic.Dto;

namespace KnowledgeBase.Logic.Services.Interfaces;

public interface IProjectService
{
    public ProjectDto Add(ProjectDto project);
    public ProjectDto Update(ProjectDto project);
    public void Remove(ProjectDto project);
    public void SoftDelete(ProjectDto project);
    public IEnumerable<ProjectDto> GetAll();
    public ProjectDto Get(Guid id);
}