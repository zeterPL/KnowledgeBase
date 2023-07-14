namespace KnowledgeBase.Logic.Dto.Resources;

public interface ICreateResourceDto
{
    public Guid UserId { get; set; }
    public IEnumerable<ProjectDto>? AssignableProjects { get; set; }
}