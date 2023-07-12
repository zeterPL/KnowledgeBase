using KnowledgeBase.Logic.Dto;

namespace KnowledgeBase.Logic.Services.Interfaces;

public interface IResourceService
{
    public void Add(ResourceDto resource);
    public ResourceDto? Get(Guid id);
    public void Remove(ResourceDto resource);
    public void Delete(ResourceDto resource);
    public void Update(ResourceDto resource);
    public IEnumerable<ResourceDto> GetAll();
}