using LibApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibApp.Repositories
{
    public class BookRepository : AbstractRepository<Book>
    {
        public BookRepository(DbContext db) : base(db)
        {
        }

        public override IList<Book> GetAll()
        {
            return db.Set<Book>()
                .Include(b => b.Genre)
                .ToList();
        }

        public override Book GetOne(int id)
        {
            return db.Set<Book>()
                .Where(b => b.Id == id)
                .Include(b => b.Genre)
                .SingleOrDefault();
        }
    }
}
