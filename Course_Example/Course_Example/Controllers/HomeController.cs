using Course_Example.Models.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Course_Example.Controllers
{
    //public class HomeController : Controller
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        //[ShareDataAttribute]
        public ActionResult About()
        {
            throw new ArgumentException("No Argument!");

            //ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            //ViewData["Message"] = "Your contact page.";
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test(String id)
        {
            return View((object)id);
        }

        public ActionResult Test2(String id)
        {
            return PartialView("Test", (object)id);
        }

        public ActionResult ContentDemo()
        {
            // SaveChange();

            return Content("<script>alert('新增成功');location.href='/';</script>");
        }

        public ActionResult Succeed()
        {
            return View("JSAlertRedirect", (object)"新增成功");
        }

        public ActionResult File1()
        {
            return File(
                @"C:\Users\wakau\Dropbox\Projects\TraPac\Stars 2.0\Final\甘特圖.xlsx",
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "客戶資料輸出.xlsx");
        }

        public ActionResult File2(int dl = 0)
        {
            if (dl == 1)
            {
                return File(Server.MapPath("~/Content/MMM.jpg"), "image/jpeg", "人生啊.jpg");
            }
            else
            {
                return File(Server.MapPath("~/Content/MMM.jpg"), "image/jpeg");
            }
        }

        public ActionResult ViewTest()
        {
            return PartialView();
        }

        public ActionResult MetroIndex()
        {
            return View();
        }

    }
}