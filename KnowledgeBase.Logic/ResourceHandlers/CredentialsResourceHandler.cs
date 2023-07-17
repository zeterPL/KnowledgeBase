using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Dto.Resources.CredentialsResource;

namespace KnowledgeBase.Logic.ResourceHandlers;

public class CredentialsResourceHandler : AbstractResourceHandler<CredentialsResourceActionDto, CredentialsResource> 
{
    public CredentialsResourceHandler(IMapper mapper) : base(mapper)
    {
    }

    protected async override Task<Resource> HandleUpdateDetails(CredentialsResourceActionDto dto, CredentialsResource model)
    {
        model.Login = dto.Login;
        model.Target = dto.Target;
        if (dto.Password != null)
        {
            model.Password = dto.Password;
        }

        return model;
    }
}