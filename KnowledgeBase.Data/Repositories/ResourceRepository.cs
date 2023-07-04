using KnowledgeBase.Data.Data;
using KnowledgeBase.Data.Models;

namespace KnowledgeBase.Data.Repositories;

public class ResourceRepository : GenericRepository<Resource>, IGenericRepository<Resource>
{
	public ResourceRepository(KnowledgeDbContext context) : base(context)
	{
	}
}
