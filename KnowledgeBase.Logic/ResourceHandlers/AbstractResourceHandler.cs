using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Dto.Resources.Interfaces;

namespace KnowledgeBase.Logic.ResourceHandlers;

public abstract class AbstractResourceHandler<TDto, TModel> : IResourceHandler 
    where TDto : IResourceAction
    where TModel : Resource
{
    private readonly IMapper _mapper;

    protected AbstractResourceHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Type ResourceType => typeof(TDto);

    protected abstract Task<Resource> HandleUpdateDetails(TDto dto, TModel model);

    public async Task<Resource> UpdateDetailsAsync<T>(T dto, Resource model) where T : IResourceAction
    {
        if (dto is not TDto resourceDto)
        {
            throw new ArgumentException("Invalid resource types for this handler");
        }

        TModel resource;
        if (dto is ICreateResourceDto)
        {
            resource = _mapper.Map<TModel>(model);
        }
        else
        {
            resource = (TModel)model;
        }

        return await HandleUpdateDetails(resourceDto, resource);
    }
}