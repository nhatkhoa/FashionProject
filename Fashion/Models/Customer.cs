using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fashion.Models
{
    public class Customer
    {
        public Guid ID { get; set; }
        public string FullName { get; set; }
        public DateTime Birth { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
