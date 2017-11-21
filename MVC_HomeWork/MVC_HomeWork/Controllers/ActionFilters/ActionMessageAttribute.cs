using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_HomeWork.Controllers.ActionFilters
{
    public class ActionMessageAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.Message = "This is APP Team number in ASP.NET MVC 5";
            base.OnActionExecuting(filterContext);
        }
    }
}