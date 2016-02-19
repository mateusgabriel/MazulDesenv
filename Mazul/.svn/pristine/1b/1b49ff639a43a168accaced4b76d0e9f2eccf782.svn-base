using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AgendaApp.App_Start
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class SessionExpireAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
       {
            HttpContext ctx = HttpContext.Current;

            if(ctx.Session != null)
            {
                if (ctx.Session.IsNewSession)
                {
                    string sessionCookie = ctx.Request.Headers["Cookie"];

                    // check  sessions here
                    if ((HttpContext.Current.Session["UsuarioAtivoId"] != null) && (sessionCookie.IndexOf("ASP.NET_SessionId") >= 0))
                    {
                        FormsAuthentication.SignOut();
                        string redirectTo = "~/Home/Login";
                        filterContext.Result = new RedirectResult(redirectTo);
                        return;
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}