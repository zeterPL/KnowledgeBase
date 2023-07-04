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
			return _resourceService.Add(resource);
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
			return _resourceService.Update(resource);
		}
	}
}
