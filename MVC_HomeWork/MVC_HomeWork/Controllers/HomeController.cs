using MVC_HomeWork.Controllers.ActionFilters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_HomeWork.Controllers
{
    [ActionMessageAttribute]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "This is APP Team number in ASP.NET MVC 5";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "國際厚生台中辦公室";

            return View();
        }

        public ActionResult AlertPost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AlertPost(String Account, String Password)
        {
            JObject jo = new JObject();
            if (string.IsNullOrWhiteSpace(Account) || string.IsNullOrWhiteSpace(Password))
            {
                jo.Add("result", false);
                jo.Add("msg", "輸入錯誤，請輸入帳號及密碼以進行驗證");
            }
            else
            {
                if (Account.Equals("20171117") && Password.Equals("20171117"))
                {
                    jo.Add("result", true);
                    jo.Add("msg", Account);
                }
                else
                {
                    jo.Add("result", false);
                    jo.Add("msg", "登入錯誤，請輸入正確的帳號密碼");
                }
            }

            return Content(JsonConvert.SerializeObject(jo), "application/json");
        }

        public ActionResult DefaultError()
        {
            throw new ArgumentException("No Argument!");

            //return View();
        }
    }
}