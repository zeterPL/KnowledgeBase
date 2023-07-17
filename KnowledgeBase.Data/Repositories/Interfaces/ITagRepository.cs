using KnowledgeBase.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Repositories.Interfaces
{
    public interface ITagRepository : IGenericRepository<Tag>
    {
        public void AddRange(IList<Tag> tags);
        public IList<Tag> GetAllByProjectId(Guid projectId);
    }
}
