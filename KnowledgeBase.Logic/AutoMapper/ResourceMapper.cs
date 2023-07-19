using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Dto.Resources;
using KnowledgeBase.Logic.Dto.Resources.CredentialsResource;
using KnowledgeBase.Logic.Dto.Resources.NoteResource;

namespace KnowledgeBase.Logic.AutoMapper;

public class ResourceMapper : Profile
{
    public ResourceMapper()
    {
        CreateMap<AzureResource, ResourceDto>();
        CreateMap<NoteResource, ResourceDto>();
        CreateMap<NoteResource, NoteResourceDto>();

        CreateMap<CredentialsResource, ResourceDto>();
        CreateMap<CredentialsResource, CredentialsResourceDto>();

        CreateMap<Resource, AzureResource>();
        CreateMap<Resource, NoteResource>();
        CreateMap<Resource, CredentialsResource>();
    }
}