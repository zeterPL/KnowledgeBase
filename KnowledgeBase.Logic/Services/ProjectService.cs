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
    private readonly IPermissionRepository permissionRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionService _permissionService;
    private readonly IUserService _userService;

    public ProjectService(IProjectRepository projectRepository, IPermissionRepository permissionRepository,
        IRoleRepository roleRepository, IPermissionService permissionService, IUserService userService)
    {
        this.projectRepository = projectRepository;
        this.permissionRepository = permissionRepository;
        _roleRepository = roleRepository;
        _permissionService = permissionService;
        _userService = userService;
    }

    private void SavePermissions(IEnumerable<Permission> permissions)
    {
        foreach (var permission in permissions)
        {
            permissionRepository.Add(permission);
        }
    }

    private static ICollection<PermissionName> DefaultCreatePermissions
    {
        get => new List<PermissionName>
        {
            PermissionName.ReadProject,
            PermissionName.EditProject,
            PermissionName.DeleteProject,
        };
    }

    private void AssignPermissionsToSuperUsers(Guid? projectId, Guid userId)
    {
        var allUsers = _userService.GetAllUsers();
        foreach(var user in allUsers) 
        {
            _userService.AddPermisionsToSpecificProject((Guid)projectId, userId);
        }
    }

    public Guid Add(ProjectDto projectDto)
    {
        Project newProject = new Project
        {
            Name = projectDto.Name,
        };
        projectRepository.Add(newProject);

        // Default permissions
        var permissions = DefaultCreatePermissions.Select(p => new Permission
        {
            PermissionName = p,
            UserId = projectDto.User.Id,
            ProjectId = newProject.Id,
        });
        SavePermissions(permissions);

        AssignPermissionsToSuperUsers(projectDto.Id, projectDto.User.Id);

        return newProject.Id;
    }

    public ProjectDto? Get(Guid id)
    {
        var project = projectRepository.Get(id);
        return project?.ToProjectDto();
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

        var project = projectRepository.Get(id);
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

        var project = projectRepository.Get(id);
        if (project == null) // Project doesnt exist
        {
            return;
        }

        projectRepository.SoftDelete(project);
    }
}