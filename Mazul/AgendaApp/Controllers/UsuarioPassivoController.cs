using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgendaApp.Entidades;
using AgendaApp.Models;

namespace AgendaApp.Controllers
{
    public class UsuarioPassivoController : Controller
    {
        UsuarioPassivoModels models = new UsuarioPassivoModels();

        // GET: UsuarioPassivo
        public ActionResult Index()
        {
            if (Session["UsuarioAtivoId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var usuariosPassivos = models.listarUsuariosPassivosPorId();
            return View(usuariosPassivos);

        }

        //GET: UsuarioPassivo/Inserir
        [HttpGet]
        public ActionResult Inserir()
        {
            if (Session["UsuarioAtivoId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        //POST: UsuarioPassivo/Inserir
        [HttpPost]
        public ActionResult Inserir(UsuarioPassivoViewModels usuarioPassivoViewModels)
        {
            if (Session["UsuarioAtivoId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                UsuarioPassivo usuarioPassivo = new UsuarioPassivo();

                usuarioPassivo.Id = usuarioPassivoViewModels.Id;
                usuarioPassivo.Nome = usuarioPassivoViewModels.Nome;
                usuarioPassivo.Sobrenome = usuarioPassivoViewModels.Sobrenome;
                usuarioPassivo.Endereco = usuarioPassivoViewModels.Endereco;
                usuarioPassivo.Email = usuarioPassivoViewModels.Email;
                usuarioPassivo.Telefone = usuarioPassivoViewModels.Telefone;
                if (usuarioPassivoViewModels.Sexo == "1")
                {
                    usuarioPassivo.Sexo = 1;
                }
                else
                {
                    usuarioPassivo.Sexo = 2;
                }

                models.inserirUsuarioPassivo(usuarioPassivo);
                TempData["Sucesso"] = "O contato foi adicionado ^^";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("FieldsError", "Alguns campos não estão preenchidos corretamente :/");
            }

            return View();
        }

        // GET: UsuarioPassivo/Editar
        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (Session["UsuarioAtivoId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            UsuarioPassivo usuarioPassivo = models.consultaUsuariosPassivosPorId((int)id);
            return View(usuarioPassivo);
        }

        // POST: UsuarioPassivo/Editar
        [HttpPost]
        public ActionResult Editar(UsuarioPassivo usuarioPassivo)
        {
            if (Session["UsuarioAtivoId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    models.editarUsuarioPassivo(usuarioPassivo);
                    TempData["Sucesso"] = "O contato foi atualizado ^^";
                    return RedirectToAction("Index");
                }
                catch (Exception e) {
                    TempData["Erro"] = "Parece que houve algum erro ao atualizar o contato ><";
                }
            }
            return View();
        }

        // GET: UsuarioPassivo/Visualizar
        [HttpGet]
        public ActionResult Visualizar(int? id)
        {
            if (Session["UsuarioAtivoId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            UsuarioPassivo usuarioPassivo = models.consultaUsuariosPassivosPorId((int)id);
            return View(usuarioPassivo);
        }

        // GET: UsuarioPassivo/Excluir
        [HttpGet]
        public ActionResult Excluir(string id)
        {
            if (Session["UsuarioAtivoId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            List<int> contatoIds = new List<int>();
            List<UsuarioPassivo> contatos = new List<UsuarioPassivo>();
            foreach (var item in id.Split(','))
            {
                contatoIds.Add(Convert.ToInt32(item));
            }

            if (contatoIds.Count > 1)
            {
                foreach (var contatoId in contatoIds)
                {
                    UsuarioPassivo contato = models.consultaUsuariosPassivosPorId((int)contatoId);
                    contatos.Add(contato);
                }
            }
            else
            {
                UsuarioPassivo contato = models.consultaUsuariosPassivosPorId((int)contatoIds[0]);
                contatos.Add(contato);
            }
            TempData["Sucesso"] = "O contato foi excluído ^^";
            return View(contatos);
        }

        [HttpGet]
        public JsonResult RecuperarDados(int? id)
        {
            try
            {
                UsuarioPassivo usuarioPassivo = models.consultaUsuariosPassivosPorId((int)id);

                var usuarioPassivoViewModels = new UsuarioPassivoViewModels();
                usuarioPassivoViewModels.Id = usuarioPassivo.Id;
                usuarioPassivoViewModels.Nome = usuarioPassivo.Nome;
                usuarioPassivoViewModels.Sobrenome = usuarioPassivo.Sobrenome;
                usuarioPassivoViewModels.Endereco = usuarioPassivo.Endereco;
                usuarioPassivoViewModels.Telefone = usuarioPassivo.Telefone;
                usuarioPassivoViewModels.Email = usuarioPassivo.Email;

                if (usuarioPassivo.Sexo == 1)
                {
                    usuarioPassivoViewModels.Sexo = "Masculino";
                }
                else
                {
                    usuarioPassivoViewModels.Sexo = "Feminino";
                }
                //usuarioPassivoViewModels.NomeUsuarioAtivo = usuarioPassivo.UsuarioAtivo.Nome;

                return Json(usuarioPassivoViewModels, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Erro de comunicação com o banco de dados - " + ex.Message);
            }
        }

        //UsuarioPassivo/DeletarUsuariosPassivos
        public JsonResult DeletarUsuariosPassivos(string ids)
        {
            try
            {
                List<int> contatoIds = new List<int>();
                foreach (var item in ids.Split(','))
                {
                    if (item != "")
                    {
                        contatoIds.Add(Convert.ToInt32(item));
                    }
                }
                if (contatoIds.Count > 1)
                {
                    foreach (var contatoId in contatoIds)
                    {
                        UsuarioPassivo contato = models.consultaUsuariosPassivosPorId((int)contatoId);
                        if (ModelState.IsValid)
                        {
                            models.excluirUsuarioPassivo(contato);
                        }
                    }
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    UsuarioPassivo contato = models.consultaUsuariosPassivosPorId((int)contatoIds[0]);
                    if (ModelState.IsValid)
                    {
                        models.excluirUsuarioPassivo(contato);
                        return Json("", JsonRequestBehavior.AllowGet);
                    }
                }
                return Json("Erro");
            }
            catch (Exception ex)
            {
                return Json("Erro de comunicação com o banco de dados - " + ex.Message);
            }
        }

    }
}