using System.Security.Claims;

namespace KnowledgeBase.Shared;

public static class ClaimsPrincipalExtensions
{
	public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
	{
		var parsed = Guid.TryParse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);
		return parsed ? userId : Guid.Empty;
	}
}