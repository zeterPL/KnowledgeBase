using KnowledgeBase.Data.Models;
using KnowledgeBase.Web.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace KnowledgeBase.Web.Policies.Handlers
{
    public class ResourcePermissionHandler : AuthorizationHandler<ResourcePermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourcePermissionRequirement requirement)
        {
            throw new NotImplementedException();
        }
    }
}
