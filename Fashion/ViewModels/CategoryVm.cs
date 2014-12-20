using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fashion.ViewModels
{
    public class CategoryVm
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [DisplayName("Tên Sản Phẩm")]
        public string Name { get; set; }

        [DisplayName("Số Sản Phẩm")]
        public int Count { get; set; }
    }
}
