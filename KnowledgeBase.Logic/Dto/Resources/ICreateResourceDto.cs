using KnowledgeBase.Data.Models.Enums;

namespace KnowledgeBase.Logic.Dto.Resources;

public interface ICreateResourceDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid ProjectId { get; set; }
    public ResourceCategory Category { get; set; }
    public Guid UserId { get; set; }
    public IEnumerable<ProjectDto>? AssignableProjects { get; set; }
}