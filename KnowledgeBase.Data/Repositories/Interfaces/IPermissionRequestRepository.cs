using KnowledgeBase.Data.Models.Interfaces;

namespace KnowledgeBase.Data.Repositories.Interfaces;

public interface IPermissionRequestRepository
{
    Task<T> GetAsync<T>(Guid id) where T : class, IPermissionRequest;

    void Update(IPermissionRequest permissionRequest);
}