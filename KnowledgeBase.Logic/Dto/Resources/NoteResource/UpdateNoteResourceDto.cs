using KnowledgeBase.Logic.Dto.Resources.Interfaces;

namespace KnowledgeBase.Logic.Dto.Resources.NoteResource;

public class UpdateNoteResourceDto : NoteResourceDto, IUpdateResourceDto
{
    public new Guid Id { get; set; }
}