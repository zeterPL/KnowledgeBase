using KnowledgeBase.Data.Data;
using KnowledgeBase.Data.Models.Interfaces;
using KnowledgeBase.Data.Repositories.Interfaces;

namespace KnowledgeBase.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly KnowledgeDbContext _context;

    protected GenericRepository(KnowledgeDbContext context)
    {
        _context = context;
    }

        public Guid Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();

            _context.Entry(entity).GetDatabaseValues();

            var IdProperty = entity.GetType().GetProperty("Id").GetValue(entity, null);
            return Guid.Parse(IdProperty.ToString());
        }

    public T? Get(Guid id)
    {
        var entity = _context.Set<T>().Find(id);
        return entity;
    }

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>();
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