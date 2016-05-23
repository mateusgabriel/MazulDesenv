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
using AgendaApp.Security;
using System.Web.UI;
using System.Web.Helpers;

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
            var usuarioAtivoViewModel = new UsuarioAtivoViewModels();
            var usuarioAtivo = models.consultarUsuariosAtivosPorId(id.Value);

            usuarioAtivoViewModel.Id = usuarioAtivo.Id;
            usuarioAtivoViewModel.Nome = usuarioAtivo.Nome;
            usuarioAtivoViewModel.Sobrenome = usuarioAtivo.Sobrenome;
            usuarioAtivoViewModel.Telefone = usuarioAtivo.Telefone;
            usuarioAtivoViewModel.Endereco = usuarioAtivo.Endereco;
            usuarioAtivoViewModel.Email = usuarioAtivo.Email;
            usuarioAtivoViewModel.Eventos = usuarioAtivo.Eventos;
            usuarioAtivoViewModel.UsuariosPassivos = usuarioAtivo.UsuariosPassivos;

            if (usuarioAtivo.Sexo == 1)
                usuarioAtivoViewModel.Sexo = "Masculino";
            else
                usuarioAtivoViewModel.Sexo = "Feminino";

            return View(usuarioAtivoViewModel);
        }

        // POST: UsuarioAtivo/Editar
        [HttpPost]
        public ActionResult Editar(UsuarioAtivoViewModels usuarioAtivoViewModels)
        {
           
            if (ModelState.IsValid)
            {
                try
                {
                    UsuarioAtivo usuarioAtivo = new UsuarioAtivo();

                    usuarioAtivo.Id = usuarioAtivoViewModels.Id;
                    usuarioAtivo.Nome = usuarioAtivoViewModels.Nome;
                    usuarioAtivo.Sobrenome = usuarioAtivoViewModels.Sobrenome;
                    usuarioAtivo.Endereco = usuarioAtivoViewModels.Endereco;
                    usuarioAtivo.Telefone = usuarioAtivoViewModels.Telefone;
                    usuarioAtivo.Email = usuarioAtivoViewModels.Email;
                    usuarioAtivo.Senha = usuarioAtivoViewModels.Senha;


                    models.editarUsuarioAtivo(usuarioAtivo);
                    TempData["Sucesso"] = "Salvo";
                    return RedirectToAction("Index");
                }
                catch(Exception e) {
                    TempData["Erro"] = "Erro ao editar";
                }
            }
            else
            {
                ModelState.AddModelError("FieldsError", "Preencha os campos corretamente.");
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


        // GET: UsuarioAtivo/Redefinir Senha
        [HttpGet]
        public ActionResult RedefinirSenha()
        {
            Uri uri = new Uri(Request.Url.ToString());
            var salt = uri.Segments[3];

            var usuarioAtivo = models.consultarUsuarioAtivoPorSalt(salt);

            if (usuarioAtivo != null)
            {
                return RedirectToAction("Editar", "UsuarioAtivo", new { id = usuarioAtivo.Id });
            }
            else {
                ModelState.AddModelError("Erro", "Link expirado.");
            }
            

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
                    try {
                        var salt = Crypto.GenerateSalt();
                        usuarioAtivo.Salt = salt;
                        models.editarUsuarioAtivo(usuarioAtivo);

                        enviarEmail(usuarioAtivo, salt);
                        TempData["Sucesso"] = "Um link para redefinição de senha foi enviado para seu email.";
                    } catch (SmtpException e) {
                        ModelState.AddModelError("Erro", e.Message);
                    }
                }
                else {
                    ModelState.AddModelError("Erro", "Usuário não encontrado.");
                }
            }
                       
            return RedirectToAction("Login", "Home");
        }

         private void enviarEmail(UsuarioAtivo usuarioAtivo, string salt) {
            string body = @"<html><body>
                                          <p>Olá! <br /><br />" + usuarioAtivo.Nome + " " + usuarioAtivo.Sobrenome + ", para redefinir sua senha click no link abaixo:</p> <p>http://localhost:6272/usuarioAtivo/redefinirsenha/" + salt + "</p> <p>Atenciosamente,</p></body></html>";

            try
            {
                MailMessage mail = new MailMessage("williamgabriel04@gmail.com", "williamgabriel04@gmail.com", "Redefinição de senha", body);
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
                Console.WriteLine(e);
                throw new SmtpException("Houve um problema e o contato não foi notificado sobre este evento.Fique tranquilo, já estamos solucionando o problema.");
            }
        }
    }
}
