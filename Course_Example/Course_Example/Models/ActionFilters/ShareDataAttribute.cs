using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Course_Example.Models.ActionFilters
{
    public class ShareDataAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.Message = "Your application description page.";

            base.OnActionExecuting(filterContext);
        }

        //public override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    filterContext.Controller.ViewBag.Message = "Your application description page.";
        //    base.OnActionExecuted(filterContext);
        //}


    }
}