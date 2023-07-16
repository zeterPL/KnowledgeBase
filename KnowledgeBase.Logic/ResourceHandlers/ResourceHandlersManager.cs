using KnowledgeBase.Logic.Dto.Resources.Interfaces;

namespace KnowledgeBase.Logic.ResourceHandlers;

public class ResourceHandlersManager
{
    private readonly IEnumerable<IResourceHandler> _handlers;

    public ResourceHandlersManager(IEnumerable<IResourceHandler> handlers)
    {
        _handlers = handlers;
    }

    public IResourceHandler GetResourceHandler<T>() where T : IResourceAction
    {
        var handler = _handlers.Single(h => h.GetResourceType() == typeof(T).BaseType);
        return handler;
    }
}