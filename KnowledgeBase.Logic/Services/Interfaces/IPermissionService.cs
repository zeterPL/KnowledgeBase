using KnowledgeBase.Data.Models.Enums;

namespace KnowledgeBase.Logic.Services.Interfaces;

public interface IPermissionService
{
	public bool UserHasProjectPermission(Guid userId, Guid projectId, ProjectPermissionName permission);
}