using KnowledgeBase.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic.Services
{
	internal interface IResourceService
	{
		public Resource Add(Resource resource);
		public Resource Update(Resource resource);
		public IEnumerable<Resource> GetAllResources();
		public void Remove(Resource rsource);

		public Resource GeById(Guid id);


	}
}
