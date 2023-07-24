using Microsoft.AspNetCore.Http;

namespace KnowledgeBase.Logic.Dto.Project;

public class CreateProjectsFromFileDto
{
    public IFormFile File { get; set; }
    public IEnumerable<ProjectDto>? ExistingProjects { get; set; }
}