using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Dto.Resources.Interfaces;
using KnowledgeBase.Logic.Dto.Resources.NoteResource;

namespace KnowledgeBase.Logic.ResourceHandlers;

public class NoteResourceHandler : IResourceHandler
{
    private readonly IMapper _mapper;

    public NoteResourceHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Type GetResourceType()
    {
        return typeof(NoteResourceDto);
    }

    public async Task<Resource> UpdateDetailsAsync<T>(T dto, Resource model) where T : IResourceAction
    {
        if (dto is not NoteResourceDto resourceDto)
        {
            throw new ArgumentException("Invalid resource types for this handler");
        }

        NoteResource resource;
        if (dto is ICreateResourceDto)
        {
            resource = _mapper.Map<NoteResource>(model);
        }
        else
        {
            resource = (NoteResource)model;
        }

        resource.Note = resourceDto.Note;

        return resource;
    }
}