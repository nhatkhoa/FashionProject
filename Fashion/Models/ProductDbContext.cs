using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGK.Models
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext()
            : base("DefaultConnection")
        {

        }
        public DbSet<Product> Products { set; get; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
