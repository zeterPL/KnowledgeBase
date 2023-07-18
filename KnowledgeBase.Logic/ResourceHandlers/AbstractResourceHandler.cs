using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Dto.Resources.Interfaces;

namespace KnowledgeBase.Logic.ResourceHandlers;

public abstract class AbstractResourceHandler<TDto, TModel> : IResourceHandler 
    where TDto : IResourceActionDto
    where TModel : Resource
{
    private readonly IMapper _mapper;

    protected AbstractResourceHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Type ResourceType => typeof(TDto);

    protected abstract Task<Resource> HandleUpdateDetails(TDto dto, TModel model);

    public async Task<Resource> UpdateDetailsAsync<T>(T dto, Resource model) where T : IResourceActionDto
    {
        if (dto is not TDto resourceDto)
        {
            throw new ArgumentException("Invalid resourc types for this handler");
        }

        TModel resource = dto switch
        {
            ICreateResourceDto => _mapper.Map<TModel>(model),
            IUpdateResourceDto => (TModel)model,
            _ => throw new ArgumentException("Invalid resource dto"),
        };
        return await HandleUpdateDetails(resourceDto, resource);
    }
}