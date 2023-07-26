using KnowledgeBase.Data.Models.Interfaces;
using KnowledgeBase.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.Data.Repositories;

public class PermissionRequestRepository : IPermissionRequestRepository
{
    protected readonly KnowledgeDbContext _context;

    public PermissionRequestRepository(KnowledgeDbContext context)
    {
        _context = context;
    }

    public async Task<T> GetAsync<T>(Guid id) where T : class, IPermissionRequest
    {
        var permission = await _context.Set<T>().SingleAsync(e => e.Id == id);
        return permission;
    }

    public void Update(IPermissionRequest permissionRequest)
    {
        _context.Update(permissionRequest);
    }
}