using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Dto;

namespace KnowledgeBase.Logic.Services.Interfaces
{
	public interface IResourceService
	{
		public void Add(ResourceDto entity);

		public Resource Get(Guid id);

		public void Remove(ResourceDto entity);

		public void Delete(ResourceDto entity);

		public void Update(ResourceDto entity);

		public IEnumerable<Resource> GetAll();
	}
}