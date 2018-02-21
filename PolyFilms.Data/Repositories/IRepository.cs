using PolyFilms.Data.Entity;
using System.Linq;

namespace PolyFilms.Data.Repositories
{
    public interface IRepository<T>
    {
        T GetById<TKey>(TKey id);

        IQueryable<T> GetAll();

        void Insert(T entity);
        
        void Update(T entity);

        void Delete(object key);

        void Update(T entity,PolyFilmsContext context);
    }
}
