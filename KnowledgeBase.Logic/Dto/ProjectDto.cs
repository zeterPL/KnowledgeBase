using KnowledgeBase.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeBase.Logic.Dto;

public class ProjectDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; }

    [Required]
    public Guid? UserId { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
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