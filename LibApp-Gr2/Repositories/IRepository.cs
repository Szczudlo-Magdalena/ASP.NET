using System.Collections.Generic;
using System.Linq;

namespace LibApp.Repositories
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll();
        T GetOne(int id);
        void Add(T item);
        void Delete(int id);
        void Save();

        void Delete(T entity)
        {
            Delete((int)entity.GetType().GetProperty("Id").GetValue(entity));
        }
    }
}
