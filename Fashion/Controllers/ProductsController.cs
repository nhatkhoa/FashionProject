﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fashion.Models;

namespace Fashion.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private ProductDbContext db = new ProductDbContext();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Brand).Include(p => p.Category);
            return View(products.ToList());
        }

        // --- Upload Ảnh
        public ActionResult UploadImage(Guid? id)
        {
            ViewBag.Id = id;
            // --- Truy vấn sản phẩm có id truyền vào
            var product = db.Products.Find(id);

            return View(product);
        }

        [HttpPost]
        public ActionResult UploadImage(Guid? id ,HttpPostedFileBase[] files)
        {
            // --- Truy vấn sản phẩm trùng id
            var sp = db.Products.Find(id.Value);
            try
            {
                foreach (var file in files)
                {
                    // --- Lấy dường dẫn image
                    string link = "SanPham/" + Guid.NewGuid() +".jpg";
                    // --- Lưu image
                    file.SaveAs(Server.MapPath("~/" + link));
                    // --- Thêm hình ảnh mới
                    db.Images.Add(new Image() {Link = link, Product = sp});
                    db.SaveChanges();
                }
                
            }
            catch(Exception e)
            {
                ViewBag.Status = "Lỗi : " + e.Message;
            }
            return View(sp);
        }

        // --- Xóa hình
        public ActionResult XoaHinh(Guid? id)
        {
            // --- Truy vấn đến hình ảnh
            var img = db.Images.FirstOrDefault(p => p.ID.Equals(id.Value));
            // --- Lấy id sản phẩm
            var sp = img.ProductID;
            // --- Xóa hình
            db.Images.Remove(img);
            db.SaveChanges();
            return RedirectToAction("UploadImage", new {id = sp});
        }
        // GET: Products/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.BrandID = new SelectList(db.Brands, "ID", "Name");
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Info,Color,Size,CategoryID,BrandID,Cost,Instock")] Product product)
        {
            product.Category = db.Categories.Single(p => p.ID.Equals(product.CategoryID));
            product.Brand = db.Brands.Single(p => p.ID.Equals(product.BrandID));
            db.Products.Add(product);
            db.SaveChanges();

            // --- Cập nhật lại sản phẩm
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Products/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandID = new SelectList(db.Brands, "ID", "Name", product.BrandID);
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Info,Color,Size,CategoryID,BrandID,Cost,Instock")] Product product)
        {
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
           
        }

        // GET: Products/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
