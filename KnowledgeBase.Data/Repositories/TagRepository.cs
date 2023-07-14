using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Repositories
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        public TagRepository(KnowledgeDbContext context) : base(context)
        {
        }

        public void AddRange(IList<Tag> tags)
        {
            _context.Set<Tag>().AddRange(tags); 
            _context.SaveChanges();
        }

        public IList<Tag> GetAllByProjectId(Guid projectId)
        {
            return _context.Set<ProjectTag>().Where(pt => pt.ProjectId == projectId)
                .Select(pt => pt.Tag).ToList();
        }
    }
}
