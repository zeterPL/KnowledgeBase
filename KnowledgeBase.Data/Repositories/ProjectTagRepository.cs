using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Repositories
{
    public class ProjectTagRepository : GenericRepository<ProjectTag>, IProjectTagRepository
    {
        public ProjectTagRepository(KnowledgeDbContext context) : base(context)
        {
        }

        public IList<ProjectTag> GetByProjectId(Guid projectId)
        {
            return _context.Set<ProjectTag>().Where(pt => pt.ProjectId == projectId).ToList();
        }

        public IList<ProjectTag> GetByTagtId(Guid TagId)
        {
            return _context.Set<ProjectTag>().Where(pt => pt.TagId == TagId).ToList();
        }

        public void RemoveByTagAndProjectId(Guid tagId, Guid projectId)
        {
            ProjectTag pt = _context.Set<ProjectTag>().Where(pt => pt.ProjectId == projectId && pt.TagId == tagId).FirstOrDefault();
            if (pt == null) { return;  }
            else _context.Set<ProjectTag>().Remove(pt);

            _context.SaveChanges();
        }
    }
}
