using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Services.Interfaces;
using System.Web.Mvc;

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
		}

		public IEnumerable<Resource> GetAllResources()
		{
			return _resourceRepository.GetAll();
		}

		public void Remove(Resource rsource)
		{
			_resourceRepository.Remove(rsource);
		}

		public void Deleted(Resource rsource)
		{
			_resourceRepository.Deleted(rsource);
		}

		public void Update(Resource resource)
		{
            _resourceRepository.Update(resource);
		}

		public IEnumerable<Resource> GetAll()
		{
			IEnumerable<Resource> resourceList = _resourceRepository.GetAll();
			return resourceList.ToList();
		}
	}
}



