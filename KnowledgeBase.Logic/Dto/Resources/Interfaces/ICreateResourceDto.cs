using KnowledgeBase.Data.Models.Enums;

namespace KnowledgeBase.Logic.Dto.Resources.Interfaces;

public interface ICreateResourceDto : IResourceActionDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ResourceCategory Category { get; set; }
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public IEnumerable<ProjectDto>? AssignableProjects { get; set; }
    IEnumerable<ResourceCategory> AssignableCategories { get; }
}