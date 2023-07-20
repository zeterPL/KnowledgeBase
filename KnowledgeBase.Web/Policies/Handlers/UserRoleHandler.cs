using Microsoft.AspNetCore.Authorization;

namespace KnowledgeBase.Web.Policies.Handlers
{
    public class UserRoleHandler : AuthorizationHandler<UserRoleRequirement>
    {
    }
}
