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
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public ProjectService(IProjectRepository projectRepository, IUserProjectPermissionRepository permissionRepository,
        IUserRepository userRepository, IRoleRepository roleRepository)
    {
        this.projectRepository = projectRepository;
        this.permissionRepository = permissionRepository;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    #region private methods

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

    private void AddPermisionsToSpecificProject(Guid projectId, Guid userId)
    {
        var user = _userRepository.Get(userId);
        var role = _roleRepository.Get(user.RoleId);
        var roleName = role.Name;
        List<UserProjectPermission> permissions = new List<UserProjectPermission>();
        if (roleName == UserRoles.SuperAdmin.ToString())
        {
            UserProjectPermission perm = new UserProjectPermission
            {
                UserId = userId,
                ProjectId = projectId,
                PermissionName = ProjectPermissionName.ReadProject
            };
            permissions.Add(perm);

            UserProjectPermission perm1 = new UserProjectPermission
            {
                UserId = userId,
                ProjectId = projectId,
                PermissionName = ProjectPermissionName.EditProject
            };
            permissions.Add(perm1);

            UserProjectPermission perm2 = new UserProjectPermission
            {
                UserId = userId,
                ProjectId = projectId,
                PermissionName = ProjectPermissionName.DeleteProject
            };
            permissions.Add(perm2);
        }
        if (roleName == UserRoles.Admin.ToString())
        {
            UserProjectPermission perm = new UserProjectPermission
            {
                UserId = userId,
                ProjectId = projectId,
                PermissionName = ProjectPermissionName.ReadProject
            };
            permissions.Add(perm);

            UserProjectPermission perm1 = new UserProjectPermission
            {
                UserId = userId,
                ProjectId = projectId,
                PermissionName = ProjectPermissionName.EditProject
            };
            permissions.Add(perm1);
        }
        permissionRepository.AddRange(permissions);
    }

    private void AssignPermissionsToSuperUsers(Guid? projectId, Guid userId)
    {
        var allUsers = _userRepository.GetAll();
        foreach (var user in allUsers)
        {
            AddPermisionsToSpecificProject((Guid)projectId, userId);
        }
    }

    #endregion private methods

    #region public methods

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

        AssignPermissionsToSuperUsers(projectDto.Id, projectDto.UserId.ToGuid());

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

    public Guid UpdateWithoutUserId(ProjectDto projectDto)
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

    public IEnumerable<ProjectDto> GetAllReadableByUser(Guid userId)
    {
        var projects = projectRepository.GetAllReadableByUser(userId);
        return projects.Select(p => p.ToProjectDto());
    }

    #endregion public methods
}