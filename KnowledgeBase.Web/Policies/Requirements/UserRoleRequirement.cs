using KnowledgeBase.Data.Models.Enums;
using Microsoft.AspNetCore.Authorization;

namespace KnowledgeBase.Web.Policies.Requirements
{
    public class UserRoleRequirement : IAuthorizationRequirement
    {
        public UserRoles UserRoleName { get; set; }

        public UserRoleRequirement(UserRoles userRoleName)
        {
            UserRoleName = userRoleName;
        }
    }
}
