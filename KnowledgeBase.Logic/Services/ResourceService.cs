using KnowledgeBase.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic.Services
{
	internal class ResourceService : IResourceService
	{
		private readonly IResourceService _resourceService;

		public ResourceService(IResourceService resourceService)
		{
			_resourceService = resourceService;
		}

		public Resource Add(Resource resource)
		{
			 _resourceService.Add(resource);
			return _resourceService.GeById(resource.Id);
		}

		public IEnumerable<Resource> GetAllResources()
		{
			return _resourceService.GetAllResources();
		}

		public void Remove(Resource rsource)
		{
			_resourceService.Remove(rsource);
		}

		public Resource Update(Resource resource)
		{
			 _resourceService.Update(resource);
			return _resourceService.GeById(resource.Id);
		}

		public Resource GetById(Guid id)
		{
			return _resourceService.GeById(id);
		}
	}
}
