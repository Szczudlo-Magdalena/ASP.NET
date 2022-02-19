using LibApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Repositories
{
    public class GenreRepository : AbstractRepository<Genre>
    {
        public GenreRepository(DbContext db) : base(db)
        {
        }
    }
}
