using System.Security.Claims;

namespace KnowledgeBase.Shared;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        return new Guid(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
    }
}