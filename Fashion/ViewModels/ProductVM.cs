using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace Fashion.ViewModels
{
    public class ProductVm
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public double Cost { get; set; }
        public string Image { get; set; } 
    }
}
