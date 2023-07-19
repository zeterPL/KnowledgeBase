using KnowledgeBase.Data.Models.Enums;
using Microsoft.AspNetCore.Authorization;

namespace KnowledgeBase.Web.Policies.Requirements
{
    public class ResourcePermissionRequirement : IAuthorizationRequirement
    {
        public ResourcePermissionName PermissionName { get; set; }
        public ResourcePermissionRequirement(ResourcePermissionName permissionName)
        {
            PermissionName = permissionName;
        }
    }
}
