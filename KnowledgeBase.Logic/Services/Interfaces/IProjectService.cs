using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Dto.Project;

namespace KnowledgeBase.Logic.Services.Interfaces;

public interface IProjectService
{
    public Guid Add(ProjectDto project);

    public Guid UpdateWithoutUserId(ProjectDto project);

    public void SoftDelete(ProjectDto project);

    public IEnumerable<ProjectDto> GetAll();

    public IEnumerable<ProjectDto> GetAllReadableByUser(Guid userId);

    public ProjectDto? Get(Guid id);

    public IList<TagDto> GetAllTagsByProjectId(Guid projectId);

    public void AddTagToProject(TagDto tagDto, Guid projectId);

    public void RemoveTagFromProject(TagDto tagDto, Guid projectId);
    public IEnumerable<ProjectDto> FindProjects(ProjectDto project, Guid userId);
    public Task<IEnumerable<Guid>> AddProjectsFromFileAsync(CreateProjectsFromFileDto dto, Guid userId);
}