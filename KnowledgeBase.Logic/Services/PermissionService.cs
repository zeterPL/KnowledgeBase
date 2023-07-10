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
    private readonly IPermissionRepository _permissionRepsitory;

    public PermissionService(IUserRepository userRepository, IProjectRepository projectRepository, IPermissionRepository permissionRepository)
    {
        _userRepository = userRepository;
        _projectRepository = projectRepository;
        _permissionRepsitory = permissionRepository;
    }

    public IList<PermissionDto> GetPermissionsbyUserId(Guid userId)
    {
       return _permissionRepsitory.GetAll().Where(p => p.UserId == userId)
            .Select(p => p.ToPermissionDto()).ToList();
    }

    public bool UserHadProjectPermission(Guid userId, Guid projectId, PermissionName permission)
    {
        User? user = _userRepository.Get(userId);
        if (user == null)
        {
            return false;
        }

        Project? project = _projectRepository.GetProjectWithPermissions(projectId);
        if (project == null)
        {
            return false;
        }

        var permissions = project.UsersPermissions.Where(p => p.UserId == userId);
        return permissions.Any(p => p.PermissionName == permission);
    }
}