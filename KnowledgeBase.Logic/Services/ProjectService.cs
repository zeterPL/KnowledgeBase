using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;

namespace KnowledgeBase.Logic.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        this.projectRepository = projectRepository;
    }

    public ProjectDto Add(ProjectDto projectDto)
    {
        Project newProject = new Project
        {
            Name = projectDto.Name,
        };
        projectRepository.Add(newProject);

        projectDto.Id = newProject.Id;
        return projectDto;
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

    public ProjectDto Update(ProjectDto projectDto)
    {
        Project project = projectRepository.Get(projectDto.Id);
        
        // Update project prop using projectDto props
        project.Name = projectDto.Name;
        
        projectRepository.Update(project);
        return project.ToProjectDto();
    }

    public void SoftDelete(ProjectDto projectDto)
    {
        Project project = projectRepository.Get(projectDto.Id);
        projectRepository.SoftDelete(project);
    }
}