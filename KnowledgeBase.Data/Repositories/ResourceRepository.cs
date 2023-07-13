using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.Data.Repositories;

public class ResourceRepository : GenericRepository<Resource>, IResourceRepository
{
	public ResourceRepository(KnowledgeDbContext context) : base(context)
	{
	}

    public void SoftDelete(Resource resource)
    {
        resource.IsDeleted = true;
        Update(resource);
    }

    public async Task<Resource?> GetResourceWithProjectAsync(Guid id)
    {
        var set = GetSet();
        var resource = await set.Include(r => r.Project)
            .Where(r => !r.IsDeleted)
            .SingleAsync(r => r.Id == id);
        return resource;
    }
}