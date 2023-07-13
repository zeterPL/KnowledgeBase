using KnowledgeBase.Data.Models.Enums;
using Microsoft.AspNetCore.Authorization;

namespace KnowledgeBase.Web.Policies.Requirements;

public class ProjectPermissionRequirement : IAuthorizationRequirement
{
	public ProjectPermissionName PermissionName { get; }

	public ProjectPermissionRequirement(ProjectPermissionName permissionName)
	{
		PermissionName = permissionName;
	}
}