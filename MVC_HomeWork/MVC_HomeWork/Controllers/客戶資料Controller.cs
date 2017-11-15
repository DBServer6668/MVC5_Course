using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_HomeWork.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ClosedXML.Excel;

namespace MVC_HomeWork.Controllers
{
    public class 客戶資料Controller : Base客戶資料Controller
    {
        // GET: 客戶資料
        //public ActionResult Index()
        //{
        //    return View(db.客戶資料.ToList());
        //}
        public ActionResult Index(String keyword, String dropDownList)
        {
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
            ViewBag.categoryList = getdropDownList();
            ViewData.Model = dataStoSort(id);

            return View("Index");
        }
        
        public ActionResult Delete_View()
        {
            //ViewBag.categoryList = getdropDownList();
            ViewData.Model = db.客戶資料.Where(P => P.是否已刪除 == true);

            return View();
        }
        
        //[HttpPost]
        //public ActionResult HasData()
        //{
        //    JObject jo = new JObject();
        //    bool result = !db.客戶資料.Count().Equals(0);
        //    jo.Add("Msg", result.ToString());

        //    return Content(JsonConvert.SerializeObject(jo), "application/json");
        //}

        public ActionResult Export()
        {
            var exportSpource = GetExportDataWithAllColumns();
            var dt = JsonConvert.DeserializeObject<DataTable>(exportSpource.ToString());

            var exportFileName =
                string.Concat("客戶資料_", DateTime.Now.ToString("yyyyMMddHHmmss"), ".xlsx");

            return new ExportExcelResult
            {
                SheetName = "客戶資料",
                FileName = exportFileName,
                ExportData = dt
            };
        }

        public ActionResult Statistics()
        {
            ViewData.Model = getStatistics();

            return View();
        }

        public ActionResult Export_Statistics()
        {
            var exportSpource = GetExportDataWithAllColumns_Statistics();
            var dt = JsonConvert.DeserializeObject<DataTable>(exportSpource.ToString());

            var exportFileName =
                string.Concat("客戶統計_", DateTime.Now.ToString("yyyyMMddHHmmss"), ".xlsx");

            return new ExportExcelResult
            {
                SheetName = "客戶統計",
                FileName = exportFileName,
                ExportData = dt
            };
        }

        public ActionResult StoSort_Statistics(int? id)
        {            
            ViewData.Model = dataStoSort_Statistics(id);

            return View("Statistics");
        }

    }
}
