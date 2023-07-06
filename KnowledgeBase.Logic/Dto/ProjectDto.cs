using KnowledgeBase.Data.Models;

namespace KnowledgeBase.Logic.Dto;

public class ProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public static class ProjectExtensions
{
    public static ProjectDto ToProjectDto(this Project project)
    {
        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
        };
    }
}