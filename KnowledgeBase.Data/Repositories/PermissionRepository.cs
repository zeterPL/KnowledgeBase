using KnowledgeBase.Data.Data;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.Data.Repositories;

public class PermissionRepository : GenericRepository<UserProjectPermission>, IPermissionRepository
{
	private readonly DbSet<UserProjectPermission> permissions;

    public PermissionRepository(KnowledgeDbContext context) : base(context)
    {
		permissions = context.Set<UserProjectPermission>();
    }

	public bool UserHasProjectPermission(Guid userId, Guid projectId, ProjectPermissionName permission)
	{
		var hasPermission = permissions.Any(p => p.UserId == userId && p.ProjectId == projectId && p.PermissionName == permission);
		return hasPermission;
	}
}
