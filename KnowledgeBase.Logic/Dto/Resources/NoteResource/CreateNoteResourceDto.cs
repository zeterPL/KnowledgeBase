using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Logic.Dto.Resources.Interfaces;

namespace KnowledgeBase.Logic.Dto.Resources.NoteResource;

public class CreateNoteResourceDto : NoteResourceDto, ICreateResourceDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ResourceCategory Category { get; set; }
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public IEnumerable<ProjectDto>? AssignableProjects { get; set; }
    public IEnumerable<ResourceCategory> AssignableCategories => new List<ResourceCategory> { ResourceCategory.Note };
}