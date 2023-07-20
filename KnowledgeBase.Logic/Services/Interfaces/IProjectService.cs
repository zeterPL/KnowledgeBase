using KnowledgeBase.Logic.Dto;

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
    public IEnumerable<ProjectDto> GetAllProjectsByTagName(TagDto tagDto, Guid userId);
    public IEnumerable<ProjectDto> GetAllProjectsByDate(string startDate, string endDate, Guid userId);
}