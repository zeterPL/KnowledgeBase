using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Dto.Resources.Interfaces;

namespace KnowledgeBase.Logic.ResourceHandlers;

public class ResourceHandlersManager
{
    private readonly IEnumerable<IResourceHandler> _handlers;

    public ResourceHandlersManager(IEnumerable<IResourceHandler> handlers)
    {
        _handlers = handlers;
    }

    private IResourceHandler GetResourceHandler<T>() where T : IResourceActionDto
    {
        var handler = _handlers.Single(h => h.ResourceType == typeof(T).BaseType);
        return handler;
    }

    public async Task<Resource> UpdateDetails<TDto>(TDto resourceDto, Resource resourceModel)
        where TDto : IResourceActionDto
    {
        return await GetResourceHandler<TDto>().UpdateDetailsAsync(resourceDto, resourceModel);
    }
}