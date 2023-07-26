using KnowledgeBase.Data.Models.Interfaces;
using KnowledgeBase.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.Data.Repositories;

public class PermissionRequestRepository : IPermissionRequestRepository
{
    private readonly KnowledgeDbContext _context;

    public PermissionRequestRepository(KnowledgeDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> AddAsync(IPermissionRequest request)
    {
        await _context.AddAsync(request);
        await _context.SaveChangesAsync();
        return request.Id;
    }

    public async Task AddRangeAsync(IEnumerable<IPermissionRequest> requests)
    {
        await _context.AddRangeAsync(requests);
        await _context.SaveChangesAsync();
    }

    public async Task<T> GetAsync<T>(Guid id) where T : class, IPermissionRequest
    {
        var permission = await _context.Set<T>().SingleAsync(e => e.Id == id);
        return permission;
    }

    public IEnumerable<T> GetRequestsSendByUser<T>(Guid userId) where T : class, IPermissionRequest
    {
        var permissions = _context.Set<T>().Where(p => p.SenderId == userId);
        return permissions;
    }

    public IEnumerable<T> GetRequestsReceivedByUser<T>(Guid userId) where T : class, IPermissionRequest
    {
        var permissions = _context.Set<T>().Where(p => p.ReceiverId == userId);
        return permissions;
    }

    public void Update(IPermissionRequest permissionRequest)
    {
        _context.Update(permissionRequest);
    }
}