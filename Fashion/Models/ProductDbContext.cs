using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fashion.Models
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext()
            : base("DefaultConnection")
        {

        }
        public DbSet<Product> Products { set; get; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Image> Images { get; set; }

        public System.Data.Entity.DbSet<Fashion.Models.Customer> Customers { get; set; }
    }
}
