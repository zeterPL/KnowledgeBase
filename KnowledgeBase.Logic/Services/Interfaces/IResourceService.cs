using KnowledgeBase.Logic.Dto.Resources;
using KnowledgeBase.Logic.Dto.Resources.AzureResource;

namespace KnowledgeBase.Logic.Services.Interfaces;

public interface IResourceService
{
    public Task AddAsync<T>(T resourceDto) where T : ICreateResourceDto;
    public ResourceDto? Get(Guid id);
    public AzureResourceDto? GetAzureResource(Guid id);
    public void SoftDelete(ResourceDto resource);
    public Task UpdateAsync(ResourceDto resource);
    public IEnumerable<ResourceDto> GetAll();
    public Task<DownloadResourceDto?> DownloadAsync(Guid id);
}