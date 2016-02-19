using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace AgendaApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            //Força expiração do cache
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //if (!User.Identity.IsAuthenticated)
            //{
                
            //}
        }

        protected void Session_End(object sender, EventArgs e)
        {
            //if (Session["UsuarioAtivoId"] != null)
            //{
            //    Response.Redirect("~/Home/Logout");
            //}
        }
    }

    //public class SessionExpireFilterAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
    //        var userName = HttpContext.Current.Session["UsuarioAtivoNome"];
    //        // check if session is supported
    //        if (userId == null && userName == null)
    //        {
    //            // check if a new session id was generated
    //            filterContext.Result = new RedirectResult("~/Home/Login");
    //            return;
    //        }

    //        base.OnActionExecuting(filterContext);
    //    }
    //}
}
