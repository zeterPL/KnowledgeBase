using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Logic.Dto.Resources.Interfaces;

namespace KnowledgeBase.Logic.Dto.Resources.CredentialsResource;

public class CreateCredentialsResourceDto : CredentialsResourceActionDto, ICreateResourceDto
{
    public IEnumerable<ProjectDto>? AssignableProjects { get; set; }
    public IEnumerable<ResourceCategory> AssignableCategories => new List<ResourceCategory> { ResourceCategory.Credentials };
    public string NewPassword { get; set; }
}