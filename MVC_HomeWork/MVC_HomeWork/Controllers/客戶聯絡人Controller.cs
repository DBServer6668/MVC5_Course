﻿using System;
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
    public class 客戶聯絡人Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        // GET: 客戶聯絡人
        //public ActionResult Index()
        //{
        //    var 客戶聯絡人 = db.客戶聯絡人.Include(客 => 客.客戶資料);
        //    return View(客戶聯絡人.ToList());
        //}
        public ActionResult Index(String keyword, int? dropDownList)
        {
            var 客戶聯絡人 = db.客戶聯絡人.Include(客 => 客.客戶資料);
            var categories = 客戶聯絡人.Where(P => P.是否已刪除 != true).OrderBy(P => P.職稱).ToList();
            SelectList categoryList = new SelectList(
                categories,
                "Id",
                "職稱"
                );

            ViewBag.categoryList = categoryList;

            if (null != keyword)
            {
                var date_客戶聯絡人 = 客戶聯絡人.Where(P => P.姓名.Contains(keyword) && P.是否已刪除 != true).ToList();
                return View("Index", date_客戶聯絡人);
            }
            else if (null != dropDownList)
            {
                var date_客戶聯絡人 = 客戶聯絡人.Where(P => P.Id == dropDownList && P.是否已刪除 != true).ToList();
                return View("Index", date_客戶聯絡人);
            }
            else
            {
                return View(客戶聯絡人.Where(P => P.是否已刪除 != true).ToList());
            }
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
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話, 是否已刪除")] 客戶聯絡人 客戶聯絡人)
        {
            if (db.客戶聯絡人.Any(p => p.客戶Id == 客戶聯絡人.客戶Id && p.姓名 == 客戶聯絡人.姓名))
            {
                ModelState.AddModelError("姓名", "測試中，本系統聯絡人姓名不可重複");
            }

            if (db.客戶聯絡人.Any(p => p.客戶Id == 客戶聯絡人.客戶Id && p.Email == 客戶聯絡人.Email))
            {
                ModelState.AddModelError("Email", "測試中，本系統聯絡人Email不可重複");
            }

            if (ModelState.IsValid)
            {
                db.客戶聯絡人.Add(客戶聯絡人);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
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
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話, 是否已刪除")] 客戶聯絡人 客戶聯絡人)
        {
            if (db.客戶聯絡人.Any(p => p.Id != 客戶聯絡人.Id && p.客戶Id == 客戶聯絡人.客戶Id && p.姓名 == 客戶聯絡人.姓名))
            {
                ModelState.AddModelError("姓名", "測試中，本系統聯絡人姓名不可重複");
            }

            if (db.客戶聯絡人.Any(p => p.Id != 客戶聯絡人.Id && p.客戶Id == 客戶聯絡人.客戶Id && p.Email == 客戶聯絡人.Email))
            {
                ModelState.AddModelError("Email", "測試中，本系統聯絡人Email不可重複");
            }

            if (ModelState.IsValid)
            {
                db.Entry(客戶聯絡人).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
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
            var 客戶聯絡人 = db.客戶聯絡人.Include(客 => 客.客戶資料);
            var data = 客戶聯絡人.Where(P => P.是否已刪除 != true).OrderBy(P => P.職稱).ToList();
            SelectList categoryList = new SelectList(
                data,
                "Id",
                "職稱"
                );

            ViewBag.categoryList = categoryList;

            switch (id)
            {
                case 1:
                    data = data.OrderBy(P => P.職稱).ToList();
                    break;
                case 2:
                    data = data.OrderBy(P => P.姓名).ToList();
                    break;
                case 3:
                    data = data.OrderBy(P => P.Email).ToList();
                    break;
                case 4:
                    data = data.OrderBy(P => P.手機).ToList();
                    break;
                case 5:
                    data = data.OrderBy(P => P.電話).ToList();
                    break;
                case 6:
                    data = data.OrderBy(P => P.客戶資料.客戶名稱).ToList();
                    break;
            }

            return View("Index", data);
        }

        public ActionResult StoSort_desc(int? id)
        {
            var 客戶聯絡人 = db.客戶聯絡人.Include(客 => 客.客戶資料);
            var data = 客戶聯絡人.Where(P => P.是否已刪除 != true).OrderBy(P => P.職稱).ToList();
            SelectList categoryList = new SelectList(
                data,
                "Id",
                "職稱"
                );

            ViewBag.categoryList = categoryList;

            switch (id)
            {
                case 1:
                    data = data.OrderByDescending(P => P.職稱).ToList();
                    break;
                case 2:
                    data = data.OrderByDescending(P => P.姓名).ToList();
                    break;
                case 3:
                    data = data.OrderByDescending(P => P.Email).ToList();
                    break;
                case 4:
                    data = data.OrderByDescending(P => P.手機).ToList();
                    break;
                case 5:
                    data = data.OrderByDescending(P => P.電話).ToList();
                    break;
                case 6:
                    data = data.OrderByDescending(P => P.客戶資料.客戶名稱).ToList();
                    break;
            }

            return View("Index", data);
        }

        public ActionResult Delete_View()
        {
            var 客戶聯絡人 = db.客戶聯絡人.Include(客 => 客.客戶資料);
            var data = 客戶聯絡人.Where(P => P.是否已刪除 != true).OrderBy(P => P.職稱).ToList();
            SelectList categoryList = new SelectList(
                data,
                "Id",
                "職稱"
                );

            ViewBag.categoryList = categoryList;
            var data_delete = 客戶聯絡人.Where(P => P.是否已刪除 == true).ToList();

            return View("Index", data_delete);
        }
    }
}
