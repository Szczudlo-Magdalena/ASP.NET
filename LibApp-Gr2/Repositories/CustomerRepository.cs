using LibApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibApp.Repositories
{
    public class CustomerRepository : AbstractRepository<Customer>
    {
        public CustomerRepository(DbContext db) : base(db)
        {
        
        }

        public override IList<Customer> GetAll()
        {
            return db.Set<Customer>()
                .Include(c => c.MembershipType)
                .ToList();
        }

        public override Customer GetOne(int id)
        {
            return db.Set<Customer>()
                .Where(c => c.Id == id)
                .Include(c => c.MembershipType)
                .SingleOrDefault();
        }
    }
}
