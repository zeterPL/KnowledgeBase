using KnowledgeBase.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic.Services.Interfaces
{
    public interface IResourceService
    {
        public void Add(Resource entity);
        public Resource Get(Guid id);
        public void Remove(Resource entity);
		public void IsRemoved(Resource entity);

		public void Update(Resource entity);

        public IEnumerable<Resource> GetAll();


    }
}
