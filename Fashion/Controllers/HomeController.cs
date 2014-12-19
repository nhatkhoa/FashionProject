using Fashion.Models;
using Fashion.ViewModels;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Fashion.Controllers
{
    public class HomeController : Controller
    {
        private ProductDbContext _db = new ProductDbContext();

        [ChildActionOnly]
        public ActionResult Category()
        {
            return View(_db.Categories.ToList());
        }

        [ChildActionOnly]
        public ActionResult Brand()
        {
           return View(_db.Brands.ToList());
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public JsonResult GetProduct(Guid id)
        {
           // --- Truy vấn đến sản phẩm trùng id
            var sp = _db.Products
                        .Where(p => p.ID.Equals(id))
                        .Select(p=> new ProductDetail()
                        {
                            // --- Chỉ lấy tên 
                            Brand = p.Brand.Name,
                            Category = p.Category.Name,
                            // --- Trả về mảng các string cắt nhau bởi khoảng trắng
                            Colors = p.Color,
                            // --- Chỉ select link của mỗi Image
                            Images = p.Images.Select(x => x.Link).ToList(),
                            Info = p.Info,
                            Cost = p.Cost,
                            ID = p.ID,
                            Sizes = p.Size,
                            InStock = p.Instock,
                            Name = p.Name
                        });
            if (sp == null)
                return null;
            return Json(sp ,JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProducts()
        {
            // --- Khởi tạo danh sách chứa sản phẩm
            // --- Lấy 9 sản phẩm và thêm vào list
            var temp = _db.Products.Take(9).Select(p => new ProductVm()
            {
                Name = p.Name,
                Category = p.Category.Name,
                Cost = p.Cost,
                ID = p.ID,
                Brand = p.Brand.Name,
                Image = p.Images.FirstOrDefault().Link
            });

            if (temp == null)
                return null;

            // --- Trả về danh sách dưới dạng JSON
            return Json(temp, JsonRequestBehavior.AllowGet);
        }
    }
}
