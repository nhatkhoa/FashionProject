namespace Fashion.Migrations
{
    using Fashion.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Fashion.Models.ProductDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Fashion.Models.ProductDbContext context)
        {
            var pro = new Product()
            {
                Brand = new Brand() {Name = "Khác"},
                Category = new Category() {Name = "Khác"},
                Name = "Áo ba lỗ",
                Info = "Áo nhiều nhiều lỗ",
                Size = "X S XL SL",
                Color = "Đen Trắng Đỏ Xanh",
                Cost = 100000,
                Instock = 100
            };

            context.Products.Add(pro);
            context.SaveChanges();
        }
    }
}
