using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Logic.Dto.Project;
using KnowledgeBase.Logic.Dto.Resources.Interfaces;

namespace KnowledgeBase.Logic.Dto.Resources.NoteResource;

public class CreateNoteResourceDto : NoteResourceActionDto, ICreateResourceDto
{
    public IEnumerable<ProjectDto>? AssignableProjects { get; set; }
    public IEnumerable<ResourceCategory> AssignableCategories => new List<ResourceCategory> { ResourceCategory.Note };
}