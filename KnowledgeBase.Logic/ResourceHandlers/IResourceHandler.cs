using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Dto.Resources;

namespace KnowledgeBase.Logic.ResourceHandlers;

public interface IResourceHandler<in TModel, in TDto, in TCreateDto>
    where TModel : Resource
    where TDto : ResourceDto
    where TCreateDto : ICreateResourceDto
{
    public Task<Guid> AddAsync(TCreateDto createDto);
    public Task<Guid> UpdateAsync(TDto dto, TModel model);
}