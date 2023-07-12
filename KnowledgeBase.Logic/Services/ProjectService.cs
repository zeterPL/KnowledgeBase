using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;
using KnowledgeBase.Shared;

namespace KnowledgeBase.Logic.Services;

public class ProjectService : IProjectService
{
	private readonly IProjectRepository projectRepository;

	public ProjectService(IProjectRepository projectRepository)
	{
		this.projectRepository = projectRepository;
	}

	public Guid Add(ProjectDto projectDto)
	{
		Project newProject = new Project
		{
			Name = projectDto.Name,
		};
		projectRepository.Add(newProject);
		return newProject.Id;
	}

	public ProjectDto? Get(Guid id)
	{
		Project project = projectRepository.Get(id);
		if (project == null)
		{
			return null;
		}
		return project.ToProjectDto();
	}

	public IEnumerable<ProjectDto> GetAll()
	{
		var projects = projectRepository.GetAll().Where(p => !p.IsDeleted);
		return projects.Select(p => p.ToProjectDto());
	}

	public Guid Update(ProjectDto projectDto)
	{
		var id = projectDto.Id.ToGuid();
		if (id == Guid.Empty)
		{
			return Guid.Empty;
		}

		Project project = projectRepository.Get(id);
		if (project == null) // Project doesnt exist
		{
			return Guid.Empty;
		}

		// Update project prop using projectDto props
		project.Name = projectDto.Name;

		projectRepository.Update(project);
		return project.Id;
	}

	public void SoftDelete(ProjectDto projectDto)
	{
		var id = projectDto.Id.ToGuid();
		if (id == Guid.Empty)
		{
			return;
		}

		Project project = projectRepository.Get(id);
		if (project == null) // Project doesnt exist
		{
			return;
		}

		projectRepository.SoftDelete(project);
	}
}