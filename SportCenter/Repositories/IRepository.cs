using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCenter.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);

        virtual void Add(T entity) {}
        virtual void Update(T entity) {}
        virtual void Delete(T entity) {}
    }
}
