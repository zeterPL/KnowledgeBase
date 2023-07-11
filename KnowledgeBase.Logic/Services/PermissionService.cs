﻿using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;

namespace KnowledgeBase.Logic.Services;

public class PermissionService : IPermissionService
{
    private readonly IUserRepository _userRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IUserProjectPermissionRepository _permissionRepsitory;

    public PermissionService(IUserRepository userRepository, IProjectRepository projectRepository, IUserProjectPermissionRepository permissionRepository)
    {
        _userRepository = userRepository;
        _projectRepository = projectRepository;
        _permissionRepsitory = permissionRepository;
    }

    public void Add(PermissionDto permission)
    {
        Permission perm = new Permission
        {
            Id = permission.Id,
            UserId = permission.UserId,
            ProjectId = permission.ProjectId,
            PermissionName = permission.PermissionName,
        };
        _permissionRepsitory.Add(perm);
    }

    public void Delete(PermissionDto permission)
    {
        var perm = _permissionRepsitory.Get(permission.Id);
        if (perm == null) { return; }

        _permissionRepsitory.Remove(perm);
    }

    public PermissionDto GetById(Guid id)
    {
        return _permissionRepsitory.Get(id).ToPermissionDto();
    }



    public PermissionDto GetPermissionsbyProjectId(Guid projectId)
    {
        return _permissionRepsitory.Get(projectId).ToPermissionDto();
    }

    public IList<PermissionDto> GetPermissionsbyUserId(Guid userId)
    {
       return _permissionRepsitory.GetAll().Where(p => p.UserId == userId)
            .Select(p => p.ToPermissionDto()).ToList();
    }

    public IList<PermissionDto> GetUserPermissionsByProjectIdAndUserId(Guid userId, Guid projectId)
    {
        var userPermissions = GetPermissionsbyUserId(userId);
        var userProjectPermissions = userPermissions.Where(p => p.ProjectId == projectId).ToList();
        return userProjectPermissions;
    }

    public bool UserHasProjectPermission(Guid userId, Guid projectId, ProjectPermissionName permission)
    { 
        return _permissionRepository.UserHasProjectPermission(userId, projectId, permission);
    }
}