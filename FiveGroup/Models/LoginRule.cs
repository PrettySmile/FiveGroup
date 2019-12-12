using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace FiveGroup.Models
{
    public class LoginRule:ActionFilterAttribute
    {
        void LoginCheck(HttpContext context)
        {
            if (context.Session["account"] == null)
            {
                context.Response.Redirect("/Home/Index");
            }
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LoginCheck(HttpContext.Current);
        }
    }
}