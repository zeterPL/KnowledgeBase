using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Dto;

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

    public void Remove(ProjectDto projectDto)
    {
        Project project = projectRepository.Get(projectDto.Id);
        projectRepository.Remove(project);
    }

    public ProjectDto Get(Guid id)
    {
        return projectRepository.Get(id).ToProjectDto();
    }

    public IEnumerable<ProjectDto> GetAll()
    {
        var projects = projectRepository.GetAll().Where(p => !p.IsDeleted);
        return projects.Select(p => p.ToProjectDto());
    }

    public Guid Update(ProjectDto projectDto)
    {
        Project project = projectRepository.Get(projectDto.Id);
        
        // Update project prop using projectDto props
        project.Name = projectDto.Name;
        
        projectRepository.Update(project);
        return project.Id;
    }

    public void SoftDelete(ProjectDto projectDto)
    {
        Project project = projectRepository.Get(projectDto.Id);
        projectRepository.SoftDelete(project);
    }
}