using LibApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Repositories
{
    public class MembershipTypeRepository : AbstractRepository<MembershipType>
    {
        public MembershipTypeRepository(DbContext db) : base(db)
        {
        }
    }
}
