using Course_Example.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Course_Example.Models
{
    //public class ProductsController : Controller
    public class ProductsController : BaseController
    {
        //private FabricsEntities db = new FabricsEntities();

        // GET: Products
        //[Authorize]
        public ActionResult Index()
        {
            //return View(db.Product.Take(10).ToList());
            if (true)
            {
                ViewData.Model = db.Product.Take(10).ToList();
            }
            else
            {
                ViewData.Model = db.Product.Take(10).ToList();
            }

            return View();

        }

        public ActionResult Search(string keyword)
        {
            var data = db.Product.Where(p => p.ProductName.Contains(keyword)).Take(10).ToList();
            return View("Index", data);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();

                TempData["ProductInsertedMsg"] = product.ProductName + " 新增成功";

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        //public ActionResult Edit([Bind(Exclude = "")] Product product)
        //延遲驗證
        public ActionResult Edit(int id, FormCollection form)
        {
            //Product product = new Product();
            Product product = db.Product.Find(id);
            //TryUpdateModel(product, new string[] { "ProductId,ProductName,Price,Active,Stock" }); //在此進行ModelBind
            //if (ModelState.IsValid)
            if (TryUpdateModel(product, new string[] { "ProductId,ProductName,Price,Active,Stock" }))
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
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

        public ActionResult CreateNew()
        {
            return View();
        }

        [HttpPost]
        [HandleError(ExceptionType = typeof(DbEntityValidationException),
            View = "Error.DbEntityValidationException")]
        public ActionResult CreateNew(ProductCreationVM data)
        {
            //if (ModelState.IsValid)
            if (true || ModelState.IsValid)
            {
                db.Product.Add(new Product()
                {
                    ProductId = 0,
                    ProductName = data.ProductName,
                    Price = data.Price,
                    Active = true,
                    //Stock = 1
                    Stock = 0
                });

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult TOP10()
        {
            var db = new FabricsEntities();

            var result = db.Product.Take(10);

            // LINQ ( C# 3.0 )
            //var data = from p in result
            //           select new ProductCreationVM()
            //           {
            //               ProductId = p.ProductId,
            //               ProductName = p.ProductName,
            //               Price = p.Price,
            //               OrderLineCount = p.OrderLine.Count()
            //           };
            //var data = "SELECT * FROM table WHERE ...";
            var data = db.Database.SqlQuery<ProductCreationVM>(
                "SELECT TOP 10*, OrderLineCount=(SELECT COUNT(*) FROM dbo.OrderLine o WHERE o.ProductId=p.ProductId) FROM dbo.Product p");

            return View(data);
        }

        public ActionResult PriceUp()
        {
            var db = new FabricsEntities();

            foreach (var item in db.Product)
            {
                item.Price += 1;
            }
            db.SaveChanges();
            //db.Database.ExecuteSqlCommand("UPDATE dbo.Product SET Price=Price+1");

            return RedirectToAction("Top10");
        }

        public ActionResult PriceUp_Each(int? id)
        {
            var db = new FabricsEntities();
            foreach (var item in db.Product)
            {
                if (null != id && id == item.ProductId)
                {
                    item.Price += 1;
                }
            }
            db.SaveChanges();

            return RedirectToAction("Top10");
        }

        public ActionResult PriceDown()
        {
            var db = new FabricsEntities();
            db.Database.ExecuteSqlCommand("UPDATE dbo.Product SET Price=Price-1");

            return RedirectToAction("Top10");
        }

        public ActionResult PriceDown_Each(int? id)
        {
            var db = new FabricsEntities();
            db.Database.ExecuteSqlCommand("UPDATE dbo.Product SET Price=Price-1 WHERE ProductId=" + id);

            return RedirectToAction("Top10");
        }
    }
}
