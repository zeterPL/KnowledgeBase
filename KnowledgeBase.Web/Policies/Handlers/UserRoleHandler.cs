using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Shared;
using KnowledgeBase.Web.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace KnowledgeBase.Web.Policies.Handlers
{
    public class UserRoleHandler : AuthorizationHandler<UserRoleRequirement>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public UserRoleHandler(IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRoleRequirement requirement)
        {
            if (context.Resource is not HttpContext httpContext)
            {
                return Task.CompletedTask;
            }

            var userId = context.User.GetUserId();
            if (userId == Guid.Empty) return Task.CompletedTask;

            var user = _userRepository.Get(userId);
            if(user is null) return Task.CompletedTask;

            var role = _roleRepository.Get(user.RoleId);
            if (role is null) return Task.CompletedTask;

            if (role.Name == requirement.UserRoleName.ToString()) context.Succeed(requirement);

            return Task.CompletedTask;

        }
    }
}
