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

            options.AddPolicy(ResourcePermission.CanReadResource, policy =>
               policy.Requirements.Add(new ResourcePermissionRequirement(ResourcePermissionName.CanRead)));
            options.AddPolicy(ResourcePermission.CanEditResource, policy =>
               policy.Requirements.Add(new ResourcePermissionRequirement(ResourcePermissionName.CanEdit)));
            options.AddPolicy(ResourcePermission.CanSaveResource, policy =>
               policy.Requirements.Add(new ResourcePermissionRequirement(ResourcePermissionName.CanSave)));
            options.AddPolicy(ResourcePermission.CanDeleteResource, policy =>
                policy.Requirements.Add(new ResourcePermissionRequirement(ResourcePermissionName.CanDelete)));
        });

        services.AddTransient<IAuthorizationHandler, ProjectPermissionHandler>();
        services.AddTransient<IAuthorizationHandler, ResourcePermissionHandler>();

        return services;
    }
}