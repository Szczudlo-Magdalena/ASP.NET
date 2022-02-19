using LibApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Repositories
{
    public class RentalRepository : AbstractRepository<Rental>
    {
        public RentalRepository(DbContext db) : base(db)
        {
        }
    }
}
