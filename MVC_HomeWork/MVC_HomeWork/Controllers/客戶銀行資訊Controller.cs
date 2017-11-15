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
    public class 客戶銀行資訊Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();
        private static string _keyword = null;

        //// GET: 客戶銀行資訊
        //public ActionResult Index()
        //{
        //    var 客戶銀行資訊 = db.客戶銀行資訊.Include(客 => 客.客戶資料);
        //    return View(客戶銀行資訊.ToList());
        //}
        public ActionResult Index(string keyword)
        {
            var 客戶銀行資訊 = db.客戶銀行資訊.Include(客 => 客.客戶資料);
            var data_銀行名稱 = 客戶銀行資訊.Where(P => P.是否已刪除 != true);

            if (null != keyword)
            {
                _keyword = keyword;
                ViewData.Model = data_銀行名稱.Where(P => P.銀行名稱.Contains(_keyword));
            }
            else
            {
                _keyword = null;
                ViewData.Model = data_銀行名稱;
            }

            return View();
        }

        // GET: 客戶銀行資訊/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = db.客戶銀行資訊.Find(id);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料.Where(P => P.是否已刪除 != true), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶銀行資訊/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼,是否已刪除")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                db.客戶銀行資訊.Add(客戶銀行資訊);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料.Where(P => P.是否已刪除 != true), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = db.客戶銀行資訊.Find(id);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料.Where(P => P.是否已刪除 != true), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼,是否已刪除")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶銀行資訊).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料.Where(P => P.是否已刪除 != true), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = db.客戶銀行資訊.Find(id);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶銀行資訊 客戶銀行資訊 = db.客戶銀行資訊.Find(id);
            //db.客戶銀行資訊.Remove(客戶銀行資訊);
            客戶銀行資訊.是否已刪除 = true;
            db.Entry(客戶銀行資訊).State = EntityState.Modified;
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
            var data = db.客戶銀行資訊.Where(P => P.是否已刪除 != true);
            if (null != _keyword)
            {
                data = data.Where(P => P.銀行名稱.Contains(_keyword));
            }

            switch (id)
            {
                case 1:
                    data = data.OrderBy(P => P.銀行名稱);
                    break;
                case 2:
                    data = data.OrderBy(P => P.銀行代碼);
                    break;
                case 3:
                    data = data.OrderBy(P => P.分行代碼);
                    break;
                case 4:
                    data = data.OrderBy(P => P.帳戶名稱);
                    break;
                case 5:
                    data = data.OrderBy(P => P.帳戶號碼);
                    break;
                case 6:
                    data = data.OrderBy(P => P.客戶資料.客戶名稱);
                    break;

                case 11:
                    data = data.OrderByDescending(P => P.銀行名稱);
                    break;
                case 22:
                    data = data.OrderByDescending(P => P.銀行代碼);
                    break;
                case 33:
                    data = data.OrderByDescending(P => P.分行代碼);
                    break;
                case 44:
                    data = data.OrderByDescending(P => P.帳戶名稱);
                    break;
                case 55:
                    data = data.OrderByDescending(P => P.帳戶號碼);
                    break;
                case 66:
                    data = data.OrderByDescending(P => P.客戶資料.客戶名稱);
                    break;
            }
            ViewData.Model = data;

            return View("Index");
        }

        public ActionResult Delete_View()
        {
            var 客戶銀行資訊 = db.客戶銀行資訊.Include(客 => 客.客戶資料);
            ViewData.Model = 客戶銀行資訊.Where(P => P.是否已刪除 == true);

            return View();
        }

        public ActionResult Export()
        {
            var exportSpource = this.GetExportDataWithAllColumns();
            var dt = JsonConvert.DeserializeObject<DataTable>(exportSpource.ToString());

            var exportFileName =
                string.Concat("客戶銀行資訊_", DateTime.Now.ToString("yyyyMMddHHmmss"), ".xlsx");

            return new ExportExcelResult
            {
                SheetName = "客戶銀行資訊",
                FileName = exportFileName,
                ExportData = dt
            };
        }

        private JArray GetExportDataWithAllColumns()
        {
            var query = db.客戶銀行資訊.Where(P => P.是否已刪除 != true).OrderBy(x => x.Id);

            JArray jObjects = new JArray();

            foreach (var item in query)
            {
                var jo = new JObject
                {
                    {"銀行名稱", item.銀行名稱},
                    {"銀行代碼", item.銀行代碼},
                    {"分行代碼", item.分行代碼},
                    {"帳戶名稱", item.帳戶名稱},
                    {"帳戶號碼", item.帳戶號碼},
                    {"客戶名稱", item.客戶資料.客戶名稱}
                };
                jObjects.Add(jo);
            }
            return jObjects;
        }
    }
}
