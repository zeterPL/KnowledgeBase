using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Services.Interfaces;
using KnowledgeBase.Shared;
using KnowledgeBase.Web.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis;

namespace KnowledgeBase.Web.Policies.Handlers
{
    public class ResourcePermissionHandler : AuthorizationHandler<ResourcePermissionRequirement>
    {
        private readonly IUserResourcePermissionService _resourcePermissonService;

        public ResourcePermissionHandler(IUserResourcePermissionService resourcePermissonService)
        {
            _resourcePermissonService = resourcePermissonService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourcePermissionRequirement requirement)
        {
            if (context.Resource is not HttpContext httpContext)
            {
                return Task.CompletedTask;
            }

            var parsed = Guid.TryParse((string?)httpContext.Request.RouteValues["Id"] ?? string.Empty, out var resourceId);
            var userId = context.User.GetUserId();


            if (!parsed || resourceId == Guid.Empty || userId == Guid.Empty)
            {
                return Task.CompletedTask;
            }

            var result = _resourcePermissonService.CheckIfUserHavePermissionToResource(resourceId, userId, requirement.PermissionName);
            if (result) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
