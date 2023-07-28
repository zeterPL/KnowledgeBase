using KnowledgeBase.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Repositories.Interfaces
{
    public interface IProjectTagRepository : IGenericRepository<ProjectTag>
    {
        public IList<ProjectTag> GetByProjectId(Guid projectId);
        public IList<ProjectTag> GetByTagtId(Guid TagId);
        public void RemoveByTagAndProjectId(Guid tagId, Guid projectId);
    }
}
