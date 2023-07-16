using KnowledgeBase.Data.Models.Enums;

namespace KnowledgeBase.Logic.Dto.Resources.Interfaces;

public interface IResourceDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid ProjectId { get; set; }
    public ResourceCategory Category { get; set; }
    public Guid UserId { get; set; }
}