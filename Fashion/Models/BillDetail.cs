using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fashion.Models
{
    public class BillDetail
    {
        public Guid ID { get; set; }
        public Guid BillID { get; set; }
        public Bill Bills { get; set; }
        public Guid ProductID { get; set; }

        public Product Products { get; set; }
        public int Count { get; set; }
        public Double Sum { get; set; }
                                                        
    }
}
