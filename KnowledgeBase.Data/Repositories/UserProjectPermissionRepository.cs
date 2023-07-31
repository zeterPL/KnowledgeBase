using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.Data.Repositories;

public class UserProjectPermissionRepository : GenericRepository<UserProjectPermission>, IUserProjectPermissionRepository
{
    private readonly DbSet<UserProjectPermission> permissions;

    public UserProjectPermissionRepository(KnowledgeDbContext context) : base(context)
    {
        permissions = context.Set<UserProjectPermission>();
    }

    public void AddRange(IEnumerable<UserProjectPermission> permissions)
    {
        _context.AddRange(permissions);
        _context.SaveChanges();
    }

    public async Task AddRangeAsync(IEnumerable<UserProjectPermission> permissions)
    {
        await GetSet().AddRangeAsync(permissions);
        await _context.SaveChangesAsync();
    }

    public bool UserHasProjectPermission(Guid userId, Guid projectId, ProjectPermissionName permission)
    {
        var hasPermission = permissions.Any(p => p.UserId == userId && p.ProjectId == projectId && p.PermissionName == permission);
        return hasPermission;
    }
}