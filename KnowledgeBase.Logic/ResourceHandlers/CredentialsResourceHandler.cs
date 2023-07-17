using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Dto.Resources.CredentialsResource;
using KnowledgeBase.Logic.Dto.Resources.Interfaces;

namespace KnowledgeBase.Logic.ResourceHandlers;

public class CredentialsResourceHandler : IResourceHandler
{
    private readonly IMapper _mapper;

    public CredentialsResourceHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Type GetResourceType()
    {
        return typeof(CredentialsResourceDto);
    }

    public async Task<Resource> UpdateDetailsAsync<T>(T dto, Resource model) where T : IResourceAction
    {
        if (dto is not CredentialsResourceDto resourceDto)
        {
            throw new ArgumentException("Invalid resource types for this handler");
        }

        CredentialsResource resource;
        if (dto is ICreateResourceDto)
        {
            resource = _mapper.Map<CredentialsResource>(model);
        }
        else
        {
            resource = (CredentialsResource)model;
        }

        resource.Login = resourceDto.Login;
        resource.Target = resourceDto.Target;
        if (resourceDto.Password != null)
        {
            resource.Password = resourceDto.Password;
        }

        return resource;
    }
}