using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibApp.Repositories
{
    public abstract class AbstractRepository<T> : IRepository<T> where T : class
    {
        protected DbContext db;

        public AbstractRepository(DbContext db)
        {
            this.db = db;
        }

        public virtual IList<T> GetAll()
        {
            return db.Set<T>().ToList();
        }

        public virtual T GetOne(int id)
        {
            return db.Find<T>(id);
        }

        public virtual void Add(T item)
        {
            db.Add(item);
        }

        public virtual void Delete(int id)
        {
            db.Remove<T>(db.Find<T>(id));
        }

        public virtual void Save()
        {
            db.SaveChanges();
        }
    }
}
