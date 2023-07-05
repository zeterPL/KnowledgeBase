namespace KnowledgeBase.Data.Repositories.Interfaces;

public interface IGenericRepository<T>
{
    public void Add(T entity);
    public T Get(Guid id);
    public void Remove(T entity);
    public void Update(T entity);
    public IEnumerable<T> GetAll();
}
