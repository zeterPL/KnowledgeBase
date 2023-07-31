using KnowledgeBase.Data.Models;

namespace KnowledgeBase.Data.Repositories.Interfaces;

public interface IResourceRepository : IGenericRepository<Resource>
{
    public void SoftDelete(Resource resource);
    public Task<Resource?> GetResourceWithProjectAsync(Guid id);
}