using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AgendaApp.App_Start;
using AgendaApp.Entidades;
using AgendaApp.Models;

namespace AgendaApp.Controllers
{
    public class HomeController : Controller
    {
        HomeModels models = new HomeModels();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // GET: Home/Login
        [HttpGet]
        //[SessionExpire]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Home/Login
        [HttpPost]
        public ActionResult Login(string email, string senha)
        {
            if (ModelState.IsValid)
            {
                
                UsuarioAtivo user = models.VerificarUsuario(email, senha);

                if (user != null)
                {
                    Session["UsuarioAtivoNome"] = user.Nome;
                    Session["UsuarioAtivoId"] = user.Id;
                    FormsAuthentication.SetAuthCookie(user.Nome, false);
                    return RedirectToAction("Index", "Evento");
                }
                else
                {
                    ModelState.AddModelError("", "O usuário ou senha estão incorretos :/");
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).ToString();
                ModelState.AddModelError("", errors);
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }
    }
}