using KnowledgeBase.Logic.Dto.Resources;

namespace KnowledgeBase.Logic.Services.Interfaces;

public interface IResourceService
{
    public Task AddAsync<T>(T resourceDto) where T : ICreateResourceDto;
    public Task UpdateAsync<T>(T resource) where T : ResourceDto;
    public T Get<T>(Guid id) where T : ResourceDto?;
    public void SoftDelete(ResourceDto resource);
    public IEnumerable<ResourceDto> GetAll();
    public Task<DownloadResourceDto?> DownloadAsync(Guid id);
}