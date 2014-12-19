using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Fashion.Models;

namespace Fashion.App_Start
{
    public class Database : DropCreateDatabaseIfModelChanges<ProductDbContext>
    {
        protected override void Seed(ProductDbContext context)
        {
            var pro = new Product()
            {
                Brand = new Brand() { Name = "Khác" },
                Category = new Category() { Name = "Khác" },
                Name = "Áo ba lỗ",
                Info = "Áo nhiều nhiều lỗ",
                Size = "X S XL SL",
                Color = "Đen Trắng Đỏ Xanh",
                Cost = 100000,
                Instock = 100
            };
            context.Products.Add(pro);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}