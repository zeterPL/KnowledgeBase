using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;
using KnowledgeBase.Shared;

namespace KnowledgeBase.Logic.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository projectRepository;
    private readonly IUserProjectPermissionRepository permissionRepository;

    public ProjectService(IProjectRepository projectRepository, IUserProjectPermissionRepository permissionRepository)
    {
        this.projectRepository = projectRepository;
        this.permissionRepository = permissionRepository;
    }

    private void SavePermissions(IEnumerable<UserProjectPermission> permissions)
    {
        foreach (var permission in permissions)
        {
            permissionRepository.Add(permission);
        }
    }

    private static ICollection<ProjectPermissionName> DefaultCreatePermissions
    {
        get => new List<ProjectPermissionName>
        {
            ProjectPermissionName.ReadProject,
            ProjectPermissionName.EditProject,
            ProjectPermissionName.DeleteProject,
        };
    }

    public Guid Add(ProjectDto projectDto)
    {
        Project newProject = new Project
        {
            Name = projectDto.Name,
        };
        projectRepository.Add(newProject);

        // Default permissions
        var permissions = DefaultCreatePermissions.Select(p => new UserProjectPermission
        {
            PermissionName = p,
            UserId = projectDto.UserId.ToGuid(),
            ProjectId = newProject.Id,
        });
        SavePermissions(permissions);

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

    public Guid UpdateWithoutUserId(ProjectDto projectDto)
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

    public IEnumerable<ProjectDto> GetAllReadableByUser(Guid userId)
    {
        var projects = projectRepository.GetAllReadableByUser(userId);
        return projects.Select(p => p.ToProjectDto());
    }
}