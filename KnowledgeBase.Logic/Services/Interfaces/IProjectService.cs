using KnowledgeBase.Logic.Dto;

namespace KnowledgeBase.Logic.Services.Interfaces;

public interface IProjectService
{
    public Guid Add(ProjectDto project);
    public Guid Update(ProjectDto project);
    public void SoftDelete(ProjectDto project);
    public IEnumerable<ProjectDto> GetAll();
    public IEnumerable<ProjectDto> GetAllReadableByUser(Guid userId);
	public Guid UpdateWithoutUserId(ProjectDto project);
	public ProjectDto? Get(Guid id);
}