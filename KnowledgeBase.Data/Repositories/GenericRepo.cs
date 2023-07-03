using KnowledgeBase.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Repositories
{
    public class GenericRepo<T> : IGenericRepository<T> where T : class
    {
        protected readonly KnowledgeDbContext _context;

        public GenericRepo(KnowledgeDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public T Get(Guid id)
        {
            var entity = _context.Set<T>().Find(id);
            return entity;
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}
