using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Logic.Dto.Resources.Interfaces;
using Microsoft.AspNetCore.Http;

namespace KnowledgeBase.Logic.Dto.Resources.AzureResource;

public class CreateAzureResourceDto : AzureResourceActionDto, ICreateResourceDto
{
    public IEnumerable<ProjectDto>? AssignableProjects { get; set; }

    public IEnumerable<ResourceCategory> AssignableCategories => new List<ResourceCategory> { ResourceCategory.Document, ResourceCategory.Documentation };
    public IFormFile NewFile { get; set; }
}