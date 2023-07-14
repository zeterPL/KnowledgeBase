using Microsoft.AspNetCore.Http;

namespace KnowledgeBase.Logic.Dto.Resources.AzureResource;

public class CreateAzureResourceDto : AzureResourceDto, ICreateResourceDto
{
    public IFormFile NewFile { get; set; }
    public IEnumerable<ProjectDto>? AssignableProjects { get; set; }
}