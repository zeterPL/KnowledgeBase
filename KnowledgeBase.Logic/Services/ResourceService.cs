using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories;

namespace KnowledgeBase.Logic.Services
{
	public class ResourceService : IResourceService
	{
		private readonly IResourceRepository _resourceRepository;

		public ResourceService(IResourceRepository resourceService)
		{
			_resourceRepository = resourceService;
		}

		public Resource Get(Guid id)
		{
			return _resourceRepository.Get(id);
		}


		public void Add(Resource resource)
		{

			_resourceRepository.Add(resource);
			_resourceRepository.Get(resource.Id);
		}

		public IEnumerable<Resource> GetAllResources()
		{
			return _resourceRepository.GetAll();
		}

		public void Remove(Resource rsource)
		{
			_resourceRepository.Remove(rsource);
		}

		public void Update(Resource resource)
		{
			_resourceRepository.Update(resource);
			_resourceRepository.Get(resource.Id);
		}

		public IEnumerable<Resource> GetAll()
		{
			IEnumerable<Resource> resourceList = _resourceRepository.GetAll();
			return resourceList.ToList();
		}

	}
}



