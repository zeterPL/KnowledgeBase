using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Dto.Resources;
using KnowledgeBase.Logic.Dto.Resources.AzureResource;

namespace KnowledgeBase.Logic.AutoMapper;

public class ResourceMapper : Profile
{
    public ResourceMapper()
    {
        CreateMap<AzureResource, ResourceDto>().ReverseMap();
        CreateMap<UpdateAzureResourceDto, ResourceDto>().ReverseMap();
    }
}