using KnowledgeBase.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
	protected readonly KnowledgeDbContext _context;

	protected GenericRepository(KnowledgeDbContext context)
	{
		_context = context;
	}

	public void Add(T entity)
	{
		_context.Set<T>().Add(entity);
		_context.SaveChanges();
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

	protected DbSet<T> GetSet()
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