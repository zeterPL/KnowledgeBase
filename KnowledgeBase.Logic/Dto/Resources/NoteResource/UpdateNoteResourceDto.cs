using KnowledgeBase.Logic.Dto.Resources.Interfaces;

namespace KnowledgeBase.Logic.Dto.Resources.NoteResource;

public class UpdateNoteResourceDto : NoteResourceActionDto, IUpdateResourceDto
{
    public new Guid Id { get; set; }
}