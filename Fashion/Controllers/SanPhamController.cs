using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fashion.Models;
using Fashion.ViewModels;

namespace Fashion.Controllers
{
    public class SanPhamController : Controller
    {
        private int SoSP = 9;
        private ProductDbContext db = new ProductDbContext();
        // GET: SanPham
        public ActionResult Index()
        {
            return Redirect("~/SanPhams/DanhMucs/1");
        }
        public ActionResult DanhMuc(int? id)
        {
            // --- ID không tồn tại thì mặc nhiên lấy danh sách của category đầu tiên
            if (!id.HasValue)
            {
                // --- Lấy id của category đầu tiên
                id = db.Categories.Take(1).SingleOrDefault().ID;
            }

            // --- Lưu trữ session
            Session["CategoryID"] = id;
            Session["Count"] = 0;

         

            
            return View();
        }

        // --- Hàm tạo danh sách danh mục cho trang sản phẩm
        [ChildActionOnly]
        public ActionResult Menu()
        {
            // --- Lấy danh sách danh mục
            return View(db.Categories.ToList());
        }

        // GET: SanPham/Brand/1
        public ActionResult Brand(int? id)
        {
            // --- ID không tồn tại thì mặc nhiên lấy danh sách của category đầu tiên
            if (!id.HasValue)
            {
                // --- Lấy id của category đầu tiên
                id = db.Categories.Take(1).SingleOrDefault().ID;
            }
            // --- Truy vấn danh sách sản phẩm của category truyền vào
            var temp = db.Products.Where(p => p.CategoryID.Equals(id));
            // --- Lấy ra tổng số trang để hiện thị sản phẩm
            ViewBag.SoTrang = temp.Count() / 9;
            ViewBag.CategoryId = id;
            return View();
        }

        public JsonResult Get(int? id)
        {
            int count = 0;
            // --- Nếu lịch sử sản phẩm có tồn tại 
            if (Session["Count"] != null)
                count = (int) Session["Count"];
           
            // --- Lấy Category đang truy vấn từ Session
            var category = (int)Session["CategoryID"];
            // --- Truy vấn danh sách sản phẩm của category truyền vào
            var temp = db.Products.Where(p => p.CategoryID.Equals(category))
                                    .OrderBy(p => p.ID)
                                    .Skip(count).Take(SoSP);
            // --- Cập nhật lại số sản phẩm trong session
            Session["Count"] = count + SoSP;

            // --- Khởi tạo danh sách ProductVm
            var list = temp.Select(p => new ProductVm()
            {
                ID = p.ID,
                Brand = p.Brand.Name,
                Category = p.Category.Name,
                Name = p.Name,
                Cost = p.Cost,
                Image = p.Images.FirstOrDefault().Link
            });

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: SanPham/GetBrand/1/0
        public JsonResult GetBrand(int id, int page)
        {

            // --- Truy vấn danh sách sản phẩm của category truyền vào
            var temp = db.Products.Where(p => p.CategoryID.Equals(id))
                                    .OrderBy(p => p.ID)
                                    .Skip((page - 1) * 9).Take(9);

            // --- Khởi tạo danh sách ProductVm
            var list = temp.Select(p => new ProductVm()
            {
                ID = p.ID,
                Brand = p.Brand.Name,
                Category = p.Category.Name,
                Name = p.Name,
                Cost = p.Cost,
                Image = p.Images.FirstOrDefault().Link
            });

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: SanPham/TimKiem/asdasd
        public JsonResult TimKiem(string tukhoa)
        {
            // --- Truy vấn danh sách sản phẩm của category truyền vào
            // --- Nếu trùng tên sản phẩm hoặc nội dung chứa key
            var temp = db.Products.Where(p => p.Name.Contains(tukhoa) || p.Info.Contains(tukhoa))
                                    .Take(9);

            // --- Khởi tạo danh sách ProductVm
            var list = temp.Select(p => new ProductVm()
            {
                ID = p.ID,
                Brand = p.Brand.Name,
                Category = p.Category.Name,
                Name = p.Name,
                Cost = p.Cost,
                Image = p.Images.FirstOrDefault().Link
            });

            return Json(tukhoa, JsonRequestBehavior.AllowGet);
        }
    }
}