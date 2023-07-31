using KnowledgeBase.Logic.Dto.Resources;
using KnowledgeBase.Logic.Dto.Resources.Interfaces;

namespace KnowledgeBase.Logic.Services.Interfaces;

public interface IResourceService
{
    public Task AddAsync<T>(T resourceDto) where T : ICreateResourceDto;
    public Task UpdateAsync<T>(T resource) where T : IUpdateResourceDto;
    public T? Get<T>(Guid id) where T : ResourceDto;
    public void SoftDelete(IResourceDto resource);
    public IEnumerable<IResourceDto> GetAll();
    public Task<DownloadResourceDto?> DownloadAsync(Guid id);
}