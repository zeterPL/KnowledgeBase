using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Dto.Resources.NoteResource;

namespace KnowledgeBase.Logic.ResourceHandlers;

public class NoteResourceHandler : AbstractResourceHandler<NoteResourceActionDto, NoteResource>
{
    public NoteResourceHandler(IMapper mapper) : base(mapper)
    {
    }

    protected override async Task<Resource> HandleUpdateDetails(NoteResourceActionDto dto, NoteResource model)
    {
        model.Note = dto.Note;
        return model;
    }
}