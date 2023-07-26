using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;

namespace KnowledgeBase.Logic.Services;

public class PermissionService : IPermissionService
{
    private readonly IUserRepository _userRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IUserProjectPermissionRepository _permissionRepository;

    public PermissionService(IUserRepository userRepository, IProjectRepository projectRepository, IUserProjectPermissionRepository permissionRepository)
    {
        _userRepository = userRepository;
        _projectRepository = projectRepository;
        _permissionRepository = permissionRepository;
    }

    public void Add(PermissionDto permission)
    {
        UserProjectPermission perm = new UserProjectPermission
        {
            UserId = permission.UserId,
            ProjectId = permission.ProjectId,
            PermissionName = permission.PermissionName,
        };
        _permissionRepository.Add(perm);
    }

    public void Delete(PermissionDto permission)
    {
        var perm = _permissionRepository.Get(permission.Id);
        if (perm == null) { return; }
        _permissionRepository.Remove(perm);
    }

    public PermissionDto GetById(Guid id)
    {
        return _permissionRepository.Get(id).ToPermissionDto();
    }

    public PermissionDto GetPermissionsbyProjectId(Guid projectId)
    {
        return _permissionRepository.Get(projectId).ToPermissionDto();
    }

    public IList<PermissionDto> GetPermissionsbyUserId(Guid userId)
    {
        return _permissionRepository.GetAll().Where(p => p.UserId == userId)
             .Select(p => p.ToPermissionDto()).ToList();
    }

    public IList<PermissionDto> GetUserPermissionsByProjectIdAndUserId(Guid userId, Guid projectId)
    {
        var userPermissions = GetPermissionsbyUserId(userId);
        var userProjectPermissions = userPermissions.Where(p => p.ProjectId == projectId).ToList();
        return userProjectPermissions;
    }

    public bool UserHadProjectPermission(Guid userId, Guid projectId, ProjectPermissionName permission)
    {
        throw new NotImplementedException();
    }

    public bool UserHasProjectPermission(Guid userId, Guid projectId, ProjectPermissionName permission)
    {
        return _permissionRepository.UserHasProjectPermission(userId, projectId, permission);
    }
}