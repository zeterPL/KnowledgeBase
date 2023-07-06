using KnowledgeBase.Data.Models;

namespace KnowledgeBase.Logic.Services.Interfaces
{
    public interface IResourceService
    {
        public void Add(Resource entity);
        public Resource Get(Guid id);
        public void Remove(Resource entity);
        public void Deleted(Resource entity);
        public void Update(Resource entity);
        public IEnumerable<Resource> GetAll();
    }
}
