using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGK.Models
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<Product> Products { set; get; }
        public DbSet<Customer> Customers { set; get; }
        public DbSet<Bill> Bills { set; get; }
        public DbSet<BillDetail> BillDetails { set; get; }

    }
}
