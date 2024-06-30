namespace SportCenter.Repositories;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T GetById(int id);

    virtual void Add(T entity) {}
    virtual void Update(T entity) {}
    virtual void Delete(T entity) {}
}
