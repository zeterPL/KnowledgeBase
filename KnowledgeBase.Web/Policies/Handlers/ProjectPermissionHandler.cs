using KnowledgeBase.Logic.Services.Interfaces;
using KnowledgeBase.Shared;
using KnowledgeBase.Web.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace KnowledgeBase.Web.Policies.Handlers;

public class ProjectPermissionHandler : AuthorizationHandler<ProjectPermissionRequirement>
{
    private readonly IPermissionService _permissionService;

    public ProjectPermissionHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        ProjectPermissionRequirement requirement)
    {
        if (context.Resource is not HttpContext httpContext)
        {
            return Task.CompletedTask;
        }

        var parsed = Guid.TryParse((string?)httpContext.Request.RouteValues["Id"] ?? string.Empty, out var projectId);
        var userId = context.User.GetUserId();

        if (!parsed || projectId == Guid.Empty || userId == Guid.Empty)
        {
            return Task.CompletedTask;
        }

        if (_permissionService.UserHasProjectPermission(userId, projectId, requirement.PermissionName))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}