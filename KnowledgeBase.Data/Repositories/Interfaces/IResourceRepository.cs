using KnowledgeBase.Data.Models;

namespace KnowledgeBase.Data.Repositories.Interfaces
{
	public interface IResourceRepository : IGenericRepository<Resource>
	{
		public void Delete(Resource resource);
	}
}