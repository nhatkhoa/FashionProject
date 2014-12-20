using System;
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
    [Authorize]
    public class CustomersController : Controller
    {
        private ProductDbContext db = new ProductDbContext();
        private ApplicationDbContext app = new ApplicationDbContext();

        // GET: Customers
        public ActionResult Index()
        {
            // --- Lấy username người dùng đang đăng nhập
            var email = User.Identity.Name;
            // --- Lấy Id ứng với user trên
            var userId = app.Users.SingleOrDefault(p => p.UserName.Contains(email)).Id;

            // --- Truy vấn thông tin khách hàng tương ứng
            var cus = db.Customers.FirstOrDefault(p => p.ID.Equals(new Guid(userId)));
            // --- Nếu chưa có thông tin khách hàng thì chuyển sang trang Create
            if (cus == null)
                return RedirectToAction("Create");
            // --- Ngược lại chuyển sang trang chi tiết
            return RedirectToAction("Details", new {id = cus.ID});
        }

        // GET: Customers/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FullName,Birth,Phone,Address")] Customer customer)
        {
            if (ModelState.IsValid)
            { 
                // --- Lấy username người dùng đang đăng nhập
                var email = User.Identity.Name;
                // --- Lấy Id ứng với user trên
                var userId = app.Users.SingleOrDefault(p => p.UserName.Contains(email)).Id;
                customer.ID = new Guid(userId);
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FullName,Birth,Phone,Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
