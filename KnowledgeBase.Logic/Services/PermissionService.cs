using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Services.Interfaces;

namespace KnowledgeBase.Logic.Services;

public class PermissionService : IPermissionService
{
	private readonly IUserProjectPermissionRepository _permissionRepository;

	public PermissionService(IUserProjectPermissionRepository permissionRepository)
	{
		_permissionRepository = permissionRepository;
	}

	public bool UserHasProjectPermission(Guid userId, Guid projectId, ProjectPermissionName permission)
	{
		return _permissionRepository.UserHasProjectPermission(userId, projectId, permission);
	}
}