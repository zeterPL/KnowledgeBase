using KnowledgeBase.Logic.Dto;

namespace KnowledgeBase.Logic.Services.Interfaces;

public interface IResourceService
{
    public Task AddAsync(ResourceDto resource);
    public ResourceDto? Get(Guid id);
    public void SoftDelete(ResourceDto resource);
    public Task UpdateAsync(ResourceDto resource);
    public IEnumerable<ResourceDto> GetAll();
    public Task<DownloadResourceDto?> DownloadAsync(Guid id);
}