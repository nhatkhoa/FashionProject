using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGK.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Bạn cần phải nhập tên Danh Mục")]
        [Display(Name = "Danh Mục")]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
