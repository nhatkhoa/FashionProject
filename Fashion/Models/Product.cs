using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fashion.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Hãy nhập tên sản phẩm.")]
        [StringLength(100)]
        [Display(Name = "Tên Sản Phẩm")]

        public string Name { get; set; }

        [StringLength(100)]
        [Display(Name = "Thông Tin Thêm")]
        public string Info { get; set; }

        [StringLength(100)]
        [Display(Name = "Màu Sắc")]
        public string Color { get; set; }
        public string Size { get; set; }
        public int CategoryID { get; set; }
        public int BrandID { get; set; }


        [Required(ErrorMessage = "Bận cần nhập giá cho sản phẩm")]
        [Display(Name = "Đơn Giá")]
        public double Cost { get; set; }

        [Display(Name = "Số Lượng")]
        [Required(ErrorMessage = "Bận cần nhập Số Lượng")]
        public int Instock { get; set; }


        [Required(ErrorMessage = "Bạn cần chọn một danh mục")]
        [Display(Name = "Danh Mục")]
        public Category Category { get; set; }

        [Required(ErrorMessage = "Bạn cần có một thương hiệu")]
        [Display(Name="Thương Hiệu")]
        public Brand Brand { get; set; }

        public virtual ICollection<Image> Images { get; set; }
        
    }
}
