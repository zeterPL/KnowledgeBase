using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;
using KnowledgeBase.Shared;

namespace KnowledgeBase.Logic.Services;

public class ResourceService : IResourceService
{
    private readonly IResourceRepository _resourceRepository;
    private readonly IMapper _mapper;

    public ResourceService(IResourceRepository resourceService, IMapper mapper)
    {
        _resourceRepository = resourceService;
        _mapper = mapper;
    }

    public ResourceDto? Get(Guid id)
    {
        var resource = _resourceRepository.Get(id);
        return _mapper.Map<ResourceDto>(resource);
    }

    public void Add(ResourceDto resourcedto)
    {
        Resource resource = _mapper.Map<Resource>(resourcedto);
        _resourceRepository.Add(resource);
    }

    public void Remove(ResourceDto resourcedto)
    {
        Resource resource = _mapper.Map<Resource>(resourcedto);
        _resourceRepository.Remove(resource);
    }

    public void Delete(ResourceDto resourceDto)
    {
        var id = resourceDto.Id.ToGuid();
        if (id == Guid.Empty)
        {
            return;
        }

        var resource = _resourceRepository.Get(id);
        if (resource == null) // Project doesnt exist
        {
            return;
        }

        _resourceRepository.Delete(resource);
    }

    public void Update(ResourceDto resourcedto)
    {
        Resource resource = _mapper.Map<Resource>(resourcedto);
        _resourceRepository.Update(resource);
    }

    public IEnumerable<ResourceDto> GetAll()
    {
        IEnumerable<Resource> resourceList = _resourceRepository.GetAll();
        return resourceList.Select(r => _mapper.Map<ResourceDto>(r)).ToList();
    }
}