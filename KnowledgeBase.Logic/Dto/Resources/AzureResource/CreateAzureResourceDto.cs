using KnowledgeBase.Logic.Dto.Resources.Interfaces;
using Microsoft.AspNetCore.Http;

namespace KnowledgeBase.Logic.Dto.Resources.AzureResource;

public class CreateAzureResourceDto : AzureResourceDto, ICreateResourceDto
{
    public IEnumerable<ProjectDto>? AssignableProjects { get; set; }
    public IFormFile NewFile { get; set; }
}