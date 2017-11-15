using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_HomeWork.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MVC_HomeWork.Controllers
{
    public class 客戶聯絡人Controller : Base客戶聯絡人Controller
    {
        // GET: 客戶聯絡人
        //public ActionResult Index()
        //{
        //    var 客戶聯絡人 = db.客戶聯絡人.Include(客 => 客.客戶資料);
        //    return View(客戶聯絡人.ToList());
        //}
        public ActionResult Index(String keyword, String dropDownList)
        {
            //var 客戶聯絡人 = db.客戶聯絡人.Include(客 => 客.客戶資料);

            if (null != dropDownList && !dropDownList.Equals("顯示全部分類"))
            {
                _dropDownList = dropDownList;
            }
            else
            {
                _dropDownList = null;
            }

            if (null != keyword)
            {
                _keyword = keyword;

            }
            else
            {
                _keyword = null;

            }

            ViewBag.categoryList = getdropDownList();

            ViewData.Model = getdata();

            return View();
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料.Where(P => P.是否已刪除 != true), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話, 是否已刪除")] 客戶聯絡人 客戶聯絡人)
        {
            //if (db.客戶聯絡人.Any(p => p.客戶Id == 客戶聯絡人.客戶Id && p.姓名 == 客戶聯絡人.姓名))
            //{
            //    ModelState.AddModelError("姓名", "測試中，本系統聯絡人姓名不可重複");
            //}

            //if (db.客戶聯絡人.Any(p => p.客戶Id == 客戶聯絡人.客戶Id && p.Email == 客戶聯絡人.Email))
            //{
            //    ModelState.AddModelError("Email", "測試中，本系統聯絡人Email不可重複");
            //}

            if (ModelState.IsValid)
            {
                db.客戶聯絡人.Add(客戶聯絡人);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料.Where(P => P.是否已刪除 != true), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料.Where(P => P.是否已刪除 != true), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話, 是否已刪除")] 客戶聯絡人 客戶聯絡人)
        {
            //if (db.客戶聯絡人.Any(p => p.Id != 客戶聯絡人.Id && p.客戶Id == 客戶聯絡人.客戶Id && p.姓名 == 客戶聯絡人.姓名))
            //{
            //    ModelState.AddModelError("姓名", "測試中，本系統聯絡人姓名不可重複");
            //}

            //if (db.客戶聯絡人.Any(p => p.Id != 客戶聯絡人.Id && p.客戶Id == 客戶聯絡人.客戶Id && p.Email == 客戶聯絡人.Email))
            //{
            //    ModelState.AddModelError("Email", "測試中，本系統聯絡人Email不可重複");
            //}

            if (ModelState.IsValid)
            {
                db.Entry(客戶聯絡人).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料.Where(P => P.是否已刪除 != true), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            //db.客戶聯絡人.Remove(客戶聯絡人);
            客戶聯絡人.是否已刪除 = true;
            db.Entry(客戶聯絡人).State = EntityState.Modified;
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
            //var 客戶聯絡人 = db.客戶聯絡人.Include(客 => 客.客戶資料);
            ViewBag.categoryList = getdropDownList();
            ViewData.Model = dataStoSort(id);

            return View("Index");
        }

        public ActionResult Delete_View()
        {
            var 客戶聯絡人 = db.客戶聯絡人.Include(客 => 客.客戶資料);
            ViewData.Model = 客戶聯絡人.Where(P => P.是否已刪除 == true);

            return View();
        }

        public ActionResult Export()
        {
            var exportSpource = GetExportDataWithAllColumns();
            var dt = JsonConvert.DeserializeObject<DataTable>(exportSpource.ToString());

            var exportFileName =
                string.Concat("客戶聯絡人_", DateTime.Now.ToString("yyyyMMddHHmmss"), ".xlsx");

            return new ExportExcelResult
            {
                SheetName = "客戶聯絡人",
                FileName = exportFileName,
                ExportData = dt
            };
        }
    }
}
