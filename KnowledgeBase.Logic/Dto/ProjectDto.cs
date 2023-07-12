using KnowledgeBase.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeBase.Logic.Dto;

public class ProjectDto
{
	public Guid? Id { get; set; }
	public string Name { get; set; }

	[Required]
	public Guid? UserId { get; set; }
}