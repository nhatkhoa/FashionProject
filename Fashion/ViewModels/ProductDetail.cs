using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fashion.ViewModels
{
    public class ProductDetail
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public int CategoryID { get; set; }
        public string Category { get; set; }
        public int BrandID { get; set; }
        public string Brand { get; set; }
        public double Cost { get; set; }
        public int InStock { get; set; }
        public string Sizes { get; set; }
        public string Colors { get; set; }
        public ICollection<string> Images { get; set; }  
    }
}
