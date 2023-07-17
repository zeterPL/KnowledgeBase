using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Dto.Resources;

namespace KnowledgeBase.Logic.AutoMapper;

public class ResourceMapper : Profile
{
    public ResourceMapper()
    {
        CreateMap<AzureResource, ResourceDto>().ReverseMap();
    }
}