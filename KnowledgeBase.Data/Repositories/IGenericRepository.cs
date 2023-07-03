namespace KnowledgeBase.Data.Repositories;

public interface IGenericRepository<T>
{
    public void Add(T entity);
    public T Get(Guid id);
    public void Remove(T entity);
    public void Update(T entity);
}
