using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_HomeWork.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "This is APP Team number in ASP.NET MVC 5";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "國際厚生台中辦公室";

            return View();
        }
    }
}