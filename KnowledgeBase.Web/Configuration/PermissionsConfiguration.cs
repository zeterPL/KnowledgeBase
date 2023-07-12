using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Web.Authorization;
using KnowledgeBase.Web.Policies.Handlers;
using KnowledgeBase.Web.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace KnowledgeBase.Web.Configuration;

public static class PermissionsConfiguration
{
	public static IServiceCollection AddPermissions(this IServiceCollection services)
	{
		services.AddAuthorization(options =>
		{
			options.AddPolicy(ProjectPermission.CanEditProject, policy =>
				policy.Requirements.Add(new ProjectPermissionRequirement(ProjectPermissionName.EditProject)));
			options.AddPolicy(ProjectPermission.CanReadProject, policy =>
				policy.Requirements.Add(new ProjectPermissionRequirement(ProjectPermissionName.ReadProject)));
			options.AddPolicy(ProjectPermission.CanDeleteProject, policy =>
				policy.Requirements.Add(new ProjectPermissionRequirement(ProjectPermissionName.DeleteProject)));
		});

		services.AddScoped<IAuthorizationHandler, ProjectPermissionHandler>();

		return services;
	}
}