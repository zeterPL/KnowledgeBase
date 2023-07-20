using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Logic.Dto.Resources.Interfaces;

namespace KnowledgeBase.Logic.Dto.Resources;

public class ResourceDto : IResourceDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid ProjectId { get; set; }
    public ResourceCategory Category { get; set; }
    public Guid UserId { get; set; }
}