﻿using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;
using KnowledgeBase.Shared;

namespace KnowledgeBase.Logic.Services;

public class ProjectService : IProjectService
{
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;
    private readonly IUserProjectPermissionRepository _permissionRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    public ProjectService(IProjectRepository projectRepository, IUserProjectPermissionRepository permissionRepository,
        IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
    {
        this._projectRepository = projectRepository;
        this._permissionRepository = permissionRepository;
        this._mapper = mapper;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    #region private methods

    private void SavePermissions(IEnumerable<UserProjectPermission> permissions)
    {
        foreach (var permission in permissions)
        {
            _permissionRepository.Add(permission);
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
        _permissionRepository.AddRange(permissions);
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
        var newProject = _mapper.Map<Project>(projectDto);        
        var newProjectId = _projectRepository.Add(newProject);

		// Default permissions
		var permissions = DefaultCreatePermissions.Select(p => new UserProjectPermission
		{
			PermissionName = p,
			UserId = projectDto.UserId.ToGuid(),
			ProjectId = newProject.Id,
		});
		SavePermissions(permissions);

        AssignPermissionsToSuperUsers(newProjectId, projectDto.UserId.ToGuid());

        return newProject.Id;
    }

    public ProjectDto? Get(Guid id)
    {
        var project = _projectRepository.Get(id);
        return _mapper.Map<ProjectDto>(project);
    }

    public IEnumerable<ProjectDto> GetAll()
    {
        var projects = _projectRepository.GetAll().Where(p => !p.IsDeleted);
        return projects.Select(p => _mapper.Map<ProjectDto>(p));
    }

    public Guid UpdateWithoutUserId(ProjectDto projectDto)
    {
        var id = projectDto.Id.ToGuid();
        if (id == Guid.Empty)
        {
            return Guid.Empty;
        }

        if (!_projectRepository.ProjectExists(id))
        {
            return Guid.Empty;
        }

        var newProject = _mapper.Map<Project>(projectDto);

        _projectRepository.Update(newProject);
        return id;
    }

	public void SoftDelete(ProjectDto projectDto)
	{
		var id = projectDto.Id.ToGuid();
		if (id == Guid.Empty)
		{
			return;
		}

        var project = _projectRepository.Get(id);
        if (project == null) // Project doesnt exist
        {
            return;
        }

        _projectRepository.SoftDelete(project);
    }

    public IEnumerable<ProjectDto> GetAllReadableByUser(Guid userId)
    {
        var projects = _projectRepository.GetAllReadableByUser(userId);
        return projects.Select(p => _mapper.Map<ProjectDto>(p));
    }

    #endregion public methods
}