using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_HomeWork.Models;

namespace MVC_HomeWork.Controllers
{
    public class 客戶資料Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        // GET: 客戶資料
        //public ActionResult Index()
        //{
        //    return View(db.客戶資料.ToList());
        //}
        public ActionResult Index(String keyword, String dropDownList)
        {
            if (null != keyword)
            {
                var data = db.客戶資料.Where(P => P.客戶名稱.Contains(keyword) && P.是否已刪除 != true).ToList();
                return View("Index", data);
            }
            else if (null != dropDownList && !dropDownList.Equals("顯示全部分類"))
            {
                var data = db.客戶資料.Where(P => P.客戶分類.Equals(dropDownList) && P.是否已刪除 != true).ToList();
                return View("Index", data);
            }
            else
            {
                return View(db.客戶資料.Where(P => P.是否已刪除 != true).ToList());
            }
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類,是否已刪除")] 客戶資料 客戶資料, string dropDownList)
        {
            if (ModelState.IsValid)
            {
                客戶資料.客戶分類 = dropDownList;
                db.客戶資料.Add(客戶資料);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email, 客戶分類, 是否已刪除")] 客戶資料 客戶資料, string dropDownList)
        {
            if (ModelState.IsValid)
            {
                客戶資料.客戶分類 = dropDownList;
                db.Entry(客戶資料).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            //db.客戶資料.Remove(客戶資料);
            客戶資料.是否已刪除 = true;
            db.Entry(客戶資料).State = EntityState.Modified;
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

        public ActionResult StoSort(int? id)
        {
            var data = db.客戶資料.Where(P => P.是否已刪除 != true).ToList();

            switch (id)
            {
                case 1:
                    data = data.OrderBy(P => P.客戶名稱).ToList();
                    break;
                case 2:
                    data = data.OrderBy(P => P.統一編號).ToList();
                    break;
                case 3:
                    data = data.OrderBy(P => P.電話).ToList();
                    break;
                case 4:
                    data = data.OrderBy(P => P.傳真).ToList();
                    break;
                case 5:
                    data = data.OrderBy(P => P.地址).ToList();
                    break;
                case 6:
                    data = data.OrderBy(P => P.Email).ToList();
                    break;
                case 7:
                    data = data.OrderBy(P => P.客戶分類).ToList();
                    break;
            }

            return View("Index", data);
        }

        public ActionResult StoSort_desc(int? id)
        {
            var data = db.客戶資料.Where(P => P.是否已刪除 != true).ToList();

            switch (id)
            {
                case 1:
                    data = data.OrderByDescending(P => P.客戶名稱).ToList();
                    break;
                case 2:
                    data = data.OrderByDescending(P => P.統一編號).ToList();
                    break;
                case 3:
                    data = data.OrderByDescending(P => P.電話).ToList();
                    break;
                case 4:
                    data = data.OrderByDescending(P => P.傳真).ToList();
                    break;
                case 5:
                    data = data.OrderByDescending(P => P.地址).ToList();
                    break;
                case 6:
                    data = data.OrderByDescending(P => P.Email).ToList();
                    break;
                case 7:
                    data = data.OrderByDescending(P => P.客戶分類).ToList();
                    break;
            }

            return View("Index", data);
        }

        public ActionResult Delete_View()
        {
            var data = db.客戶資料.Where(P => P.是否已刪除 == true).ToList();
            return View("Index", data);
        }

        public ActionResult Statistics()
        {
            var db = new 客戶資料Entities();

            var result = db.客戶資料.Where(P => P.是否已刪除 != true);

            // LINQ ( C# 3.0 )
            //var data = "SELECT * FROM table WHERE ...";
            var data = from p in result
                       select new 客戶統計()
                       {
                           客戶名稱 = p.客戶名稱,
                           銀行帳戶數量 = p.客戶銀行資訊.Count(),
                           聯絡人數量 = p.客戶聯絡人.Count()
                       };

            return View(data);
        }
    }
}
