using KnowledgeBase.Logic.Dto.Resources.Interfaces;

namespace KnowledgeBase.Logic.ResourceHandlers;

public interface IResourceHandler
{
    Task<Guid> AddAsync<T>(T resourceDto) where T : ICreateResourceDto;
    Task UpdateAsync<T>(T resourceDto) where T : IUpdateResourceDto;
    Type GetResourceType();
}