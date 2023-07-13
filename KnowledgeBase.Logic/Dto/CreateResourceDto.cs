using Microsoft.AspNetCore.Http;

namespace KnowledgeBase.Logic.Dto;

public class CreateResourceDto : ResourceDto
{
    public IFormFile NewFile { get; set; }
    public IEnumerable<ProjectDto>? AssignableProjects { get; set; }
}