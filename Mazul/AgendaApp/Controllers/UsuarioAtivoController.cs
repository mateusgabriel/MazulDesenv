using AgendaApp.Entidades;
using AgendaApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
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
                    TempData["Sucesso"] = "Bem vindo! Entre com seu login e senha e ...";
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
                try
                {
                    models.editarUsuarioAtivo(usuarioAtivo);
                    TempData["Sucesso"] = "Salvo";
                    return RedirectToAction("Index");
                }
                catch(Exception e) {
                    TempData["Erro"] = "Erro ao editar";
                }
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


        // GET: UsuarioAtivo/Index
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["UsuarioAtivoId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var usuariosAtivos = models.consultarUsuariosAtivos();
            return View(usuariosAtivos);
        }

        // GET: UsuarioAtivo/Redefinir Senha
        [HttpGet]
        public ActionResult RedefinirSenha()
        {

            Uri someUri = Page.Request.Url;
            string urlstr = string.Format(
                "AbsoluteUri: {0}<br/> Scheme: {1}<br/> Host: {2}<br/> Query: {3} ",
                someUri.AbsoluteUri, someUri.Scheme, someUri.Host, someUri.Query);

              return RedirectToAction("Login", "Home");
        }

        // POST: UsuarioAtivo/Inserir
        [HttpPost]
        public ActionResult RecuperarSenha(String login)
        {
            if (ModelState.IsValid)
            {
                UsuarioAtivo usuarioAtivo = models.consultarUsuarioAtivoPorLogin(login);

                if (usuarioAtivo != null)
                {
                    String hash = usuarioAtivo.Email + DateTime.Today.ToLongDateString() + "mazulrecuperaçãodesenha";
                    hash = converterParaMD5(hash);
                    enviarEmail(usuarioAtivo, hash);
                    TempData["Sucesso"] = "Um link para redefinição de senha foi enviado para seu email.";
                }
                else {
                    ModelState.AddModelError("Erro", "Login incorreto.");
                }
            }
            else
            {
                ModelState.AddModelError("Erro", "Preencha os campos corretamente.");
            }

            
            return RedirectToAction("Login", "Home");
        }

        private string converterParaMD5(string input)

        {

            // step 1, calculate MD5 hash from input

            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)

            {

                sb.Append(hash[i].ToString("X2"));

            }

            return sb.ToString();

        }

        private void enviarEmail(UsuarioAtivo usuarioAtivo, String hash) {
            var usuarioNome = usuarioAtivo.Nome + " " + usuarioAtivo.Sobrenome;
            var usuarioEmail = usuarioAtivo.Email;
            string body = @"<html><body>
                                          <p>Olá! <br /><br />" + usuarioNome + ", para redefinir sua senha click no link abaixo:</p> <p>http://localhost:6272/redefinirsenha/" + hash + "</p> <p>Atenciosamente,</p></body></html>";

            try
            {
                MailMessage mail = new MailMessage(usuarioEmail, usuarioEmail, "Redefinição de senha", body);
                mail.From = new MailAddress("mazulapp@gmail.com", "Mazul");
                mail.IsBodyHtml = true; // necessary if you're using html email

                NetworkCredential credential = new NetworkCredential("mazulapp@gmail.com", "Asdzxc123$");
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = credential;
                smtp.Send(mail);
            }
            catch (SmtpException e)
            {
                TempData["Error"] = "Houve um problema e o contato não foi notificado sobre este evento. Fique tranquilo, já estamos solucionando o problema.";
            }
        }
    }
}
