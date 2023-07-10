using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Services.Interfaces;

namespace KnowledgeBase.Logic.Services;

public class PermissionService : IPermissionService
{
    private readonly IUserRepository _userRepository;
    private readonly IProjectRepository _projectRepository;

    public PermissionService(IUserRepository userRepository, IProjectRepository projectRepository)
    {
        _userRepository = userRepository;
        _projectRepository = projectRepository;
    }

    public bool UserHasProjectPermission(Guid userId, Guid projectId, ProjectPermissionName permission)
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