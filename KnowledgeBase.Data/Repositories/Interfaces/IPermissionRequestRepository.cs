using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Data.Models.Interfaces;

namespace KnowledgeBase.Data.Repositories.Interfaces;

public interface IPermissionRequestRepository
{
    Task<Guid> AddAsync(IPermissionRequest request);

    Task AddRangeAsync(IEnumerable<IPermissionRequest> requests);

    Task<T> GetAsync<T>(Guid id) where T : class, IPermissionRequest;

    IEnumerable<T> GetRequestsSendByUser<T>(Guid userId) where T : class, IPermissionRequest;

    IEnumerable<T> GetRequestsReceivedByUser<T>(Guid userId) where T : class, IPermissionRequest;

    void Update(IPermissionRequest permissionRequest);

    void UpdateRange(IEnumerable<IPermissionRequest> requests);

    IEnumerable<ProjectPermissionRequest> ProjectPermissionRequestsExists(
        IEnumerable<ProjectPermissionName> requests, Guid projectId, Guid senderId);
}