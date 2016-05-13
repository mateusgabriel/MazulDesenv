using AgendaApp.Entidades;
using AgendaApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;

namespace AgendaApp.Controllers
{
    public class UsuarioAtivoController : Controller
    {
        UsuarioAtivoModels models = new UsuarioAtivoModels();

        // GET: UsuarioAtivo/Index
        [HttpGet]
        public ActionResult Index()
        {
            if(Session["UsuarioAtivoId"] == null){
                return RedirectToAction("Login", "Home");
            }
            var usuariosAtivos = models.consultarUsuariosAtivos();
            return View(usuariosAtivos);
        }

        // GET: UsuarioAtivo/Inserir
        [HttpGet]
        public ActionResult Inserir()
        {
            return View();
        }

        // POST: UsuarioAtivo/Inserir
        [HttpPost]
        public ActionResult Inserir(UsuarioAtivoViewModels usuarioAtivoViewModels)
        {
            if (ModelState.IsValid)
            {
                UsuarioAtivo usuarioAtivo = new UsuarioAtivo();

                usuarioAtivo.Id = usuarioAtivoViewModels.Id;
                usuarioAtivo.Nome = usuarioAtivoViewModels.Nome;
                usuarioAtivo.Sobrenome = usuarioAtivoViewModels.Sobrenome;
                usuarioAtivo.Endereco = usuarioAtivoViewModels.Endereco;
                usuarioAtivo.Telefone = usuarioAtivoViewModels.Telefone;
                usuarioAtivo.Email = usuarioAtivoViewModels.Email;
                usuarioAtivo.Senha = usuarioAtivoViewModels.Senha;
                if (usuarioAtivoViewModels.Sexo == "1")
                {
                    usuarioAtivo.Sexo = 1;
                }
                else
                {
                    usuarioAtivo.Sexo = 2;
                }

                if (!models.inserirUsuarioAtivo(usuarioAtivo)) 
                {
                    ModelState.AddModelError("PasswordError", "A senha deve conter ao menos 7 caracteres sendo no mínimo um numérico, um símbolo especial e uma letra maiúscula.");
                }
                else
                {
                    return RedirectToAction("Login","Home");
                }
            }
            else
            {
                ModelState.AddModelError("FieldsError", "Preencha os campos corretamente.");
            }
            return View();
        }

        // GET: UsuarioAtivo/Editar
        [HttpGet]
        public ActionResult Editar(int? id)
        {
            UsuarioAtivo usuarioAtivo = models.consultarUsuariosAtivosPorId((int)id);
            return View(usuarioAtivo);
        }

        // POST: UsuarioAtivo/Editar
        [HttpPost]
        public ActionResult Editar(UsuarioAtivo usuarioAtivo)
        {
            if (Session["UsuarioAtivoId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                models.editarUsuarioAtivo(usuarioAtivo);
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: UsuarioAtivo/Visualizar
        [HttpGet]
        public ActionResult Visualizar(int? id)
        {
            if (Session["UsuarioAtivoId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
                UsuarioAtivo usuarioAtivo = models.consultarUsuariosAtivosPorId((int)id);
                return View(usuarioAtivo);
        }

        // GET: UsuarioAtivo/Listar
        //[HttpGet]
        //public JsonResult Listar()
        //{
        //    try
        //    {
        //        var usuariosAtivos = models.consultarUsuariosAtivos();
        //        var usuariosAtivosModels = new List<UsuarioAtivoModels>();

        //        foreach (var usuarioAtivo in usuariosAtivos)
        //        {
        //            var usuarioAtivoModels = new UsuarioAtivoModels();
        //            usuarioAtivoModels.Id = usuarioAtivo.Id;
        //            usuarioAtivoModels.Nome = usuarioAtivo.Nome;
        //            usuarioAtivoModels.Sobrenome = usuarioAtivo.Sobrenome;
        //            usuarioAtivoModels.Endereco = usuarioAtivo.Endereco;
        //            usuarioAtivoModels.Telefone = usuarioAtivo.Telefone;
        //            usuarioAtivoModels.Email = usuarioAtivo.Email;
        //            usuarioAtivoModels.Sexo = usuarioAtivo.Sexo;

        //            usuariosAtivosModels.Add(usuarioAtivoModels);
        //        }
        //        return Json(usuariosAtivosModels, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json("Erro de comunicação com o banco de dados. - " + ex.Message);
        //    }
        //}

        // GET: UsuarioAtivo/RecuperarDados
        [HttpGet]
        public JsonResult RecuperarDados(int? id)
        {
            if (Session["UsuarioAtivoId"] == null)
            {
                return Json(new
                {
                    redirectUrl = Url.Action("Login", "Home"),
                    isRedirect = true
                });
            }
            try
            {
                UsuarioAtivo usuarioAtivo = models.consultarUsuariosAtivosPorId((int)id);

                var usuarioAtivoViewModels = new UsuarioAtivoViewModels();
                usuarioAtivoViewModels.Id = usuarioAtivo.Id;
                usuarioAtivoViewModels.Nome = usuarioAtivo.Nome;
                usuarioAtivoViewModels.Sobrenome = usuarioAtivo.Sobrenome;
                usuarioAtivoViewModels.Endereco = usuarioAtivo.Endereco;
                usuarioAtivoViewModels.Telefone = usuarioAtivo.Telefone;
                usuarioAtivoViewModels.Email = usuarioAtivo.Email;
                if (usuarioAtivo.Sexo == 1)
                {
                    usuarioAtivoViewModels.Sexo = "Masculino";
                }
                else
                {
                    usuarioAtivoViewModels.Sexo = "Feminino";
                }

                return Json(usuarioAtivoViewModels, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Erro de comunicação com o banco de dados. - " + ex.Message);
            }
        }

        // GET: UsuarioAtivo/Excluir
        [HttpGet]
        public JsonResult Excluir(int? id)
        { 
            if (Session["UsuarioAtivoId"] == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
                //return Json(null, JsonRequestBehavior.AllowGet);
            }
            try
            {
                UsuarioAtivo usuarioAtivo = models.consultarUsuariosAtivosPorId((int)id);
                var usuariosPassivos = usuarioAtivo.UsuariosPassivos.ToList<UsuarioPassivo>();
                if (usuariosPassivos != null)
                {
                    models.excluirTodosUsuariosPassivos(usuariosPassivos);
                }
                if (ModelState.IsValid)
                {
                    models.excluirUsuarioAtivo(usuarioAtivo);
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                return Json("Erro");
            }
            catch (Exception ex)
            {
                return Json("Erro de comunicação com o banco de dados. - " + ex.Message);
            }
        }
    }
}
