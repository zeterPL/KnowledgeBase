using KnowledgeBase.Data.Models.Enums;
using Microsoft.AspNetCore.Authorization;

namespace KnowledgeBase.Web.Policies.Requirements;

public class ProjectPermissionRequirement : IAuthorizationRequirement
{
    public PermissionName PermissionName { get; }

    public ProjectPermissionRequirement(PermissionName permissionName)
    {
        PermissionName = permissionName;
    }
}