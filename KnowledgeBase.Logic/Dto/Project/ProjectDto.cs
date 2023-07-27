﻿using System.ComponentModel.DataAnnotations;

namespace KnowledgeBase.Logic.Dto.Project;

public class ProjectDto
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }

    [Required]
    public Guid? UserId { get; set; }

    public string Description { get; set; }
    public DateTime StartDate { get; set; }
}