using KnowledgeBase.Data.Models.Interfaces;

namespace KnowledgeBase.Data.Repositories.Interfaces;

public interface IPermissionRequestRepository
{
    Task<Guid> AddAsync(IPermissionRequest request);
    Task AddRangeAsync(IEnumerable<IPermissionRequest> requests);

    Task<T> GetAsync<T>(Guid id) where T : class, IPermissionRequest;

    void Update(IPermissionRequest permissionRequest);
}