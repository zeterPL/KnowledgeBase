using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Dto.Resources.Interfaces;

namespace KnowledgeBase.Logic.ResourceHandlers;

public interface IResourceHandler
{
    Task<Resource> UpdateDetailsAsync<T>(T resourceDto, Resource resourceModel)
        where T : IResourceAction;

    public Type ResourceType { get; }
}