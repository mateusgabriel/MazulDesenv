using AgendaApp.Entidades;
using AgendaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace AgendaApp.Controllers
{
    public class EventoController : Controller
    {
        EventoModels models = new EventoModels();

        // GET: Evento
        // [AllowAnonymous]
        public ActionResult Index()
        {
            if (Session["UsuarioAtivoId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            if (TempData["ErrorMail"] != null)
            {
                ModelState.AddModelError("ErrorMail", TempData["ErrorMail"].ToString());
            }
            //Teste para pegar todos os dias de domingo no mês de Setembro => Passou!
            //var dias = models.consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Sunday);

            EventoViewModels eventosViewModel = CarregarListas();
            return View(eventosViewModel);
        }

        public int GetWeekOfMonth(DateTime TargetDate)
        {
            return (TargetDate.Day - 1) / 7 + 1;
        }

        //GET: Evento/Inserir
        [HttpGet]
        public ActionResult Inserir()
        {
            if (Session["UsuarioAtivoId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            EventoViewModels eventoModel = new EventoViewModels();
            eventoModel.UsuariosPassivos = models.listarContatosDoUsuario();
            return View(eventoModel);
        }

        //POST: Evento/Inserir
        [HttpPost]
        public ActionResult Inserir(EventoViewModels eventoViewModel, int[] contatos)
        {
            if (Session["UsuarioAtivoId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                Evento evento = new Evento();
                evento.Nome = eventoViewModel.Nome;
                evento.Horario = eventoViewModel.Horario;
                evento.DataEvento = eventoViewModel.DataEvento;
                evento.DiaDaSemana = eventoViewModel.DataEvento.DayOfWeek;
                evento.Descricao = eventoViewModel.Descricao;
                evento.Local = eventoViewModel.Local;
                evento.Periodicidade = eventoViewModel.Periodicidade;
                evento.SemanaDoMes = models.consultaSemanaDoMes(DateTime.Today);
                evento.UsuarioAtivo = models.retornarUsuarioLogado();

                if (contatos != null)
                {
                    var usuariosPassivos = new List<UsuarioPassivo>();
                    List<string> contatosEmail = new List<string>();
                    int i = 0;

                    foreach (var contatoId in contatos)
                    {
                        UsuarioPassivo contato = models.consultaUsuariosPassivosPorId(contatoId);
                        contatosEmail.Add(contato.Email);
                        usuariosPassivos.Add(contato);
                    }
                    evento.UsuariosPassivos = usuariosPassivos;

                    foreach (var email in contatosEmail)
                    {
                        var usuarioNome = evento.UsuarioAtivo.Nome + " " + evento.UsuarioAtivo.Sobrenome;
                        var usuarioEmail = evento.UsuarioAtivo.Email;
                        string body = @"<html><body>
                                            <p>Olá! <br/><br/>" + usuarioNome + " marcou " + evento.Nome + " com você no(a) " + evento.Local +
                                                           " no dia " + evento.DataEvento.ToShortDateString() + " às " + evento.Horario.ToString(@"hh\:mm") +
                                            ".</p><p>" + evento.Descricao + "</p> <p>Atenciosamente,</p></body></html>";

                        try
                        {
                            MailMessage mail = new MailMessage(usuarioEmail, email, evento.Nome, body);
                            mail.From = new MailAddress(usuarioEmail, usuarioNome);
                            mail.IsBodyHtml = true; // necessary if you're using html email

                            NetworkCredential credential = new NetworkCredential("mazulapp@gmail.com", "Asdzxc123$");
                            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                            smtp.EnableSsl = true;
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = credential;
                            smtp.Send(mail);
                            i++;
                        }
                        catch (SmtpException e)
                        {
                            TempData["ErrorMail"] = "Houve um problema e o contato não foi notificado sobre este evento. Fique tranquilo, já estamos solucionando o problema.";
                            //ModelState.AddModelError("ErrorMail", "Ocorreu um erro ao enviar email para contato. Fique tranquilo, já estamos solucionando o problema." + e);
                        }

                    }
                }

                models.inserirEvento(evento);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("erro", "Preencha os campos corretamente.");
            }
            return View();
        }

        // GET: Evento/Editar
        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (Session["UsuarioAtivoId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Evento evento = models.consultarEventosPorId((int)id);
            List<int> contatos = new List<int>();
            foreach (var item in evento.UsuariosPassivos)
            {
                int contato = item.Id;
                contatos.Add(contato);
            }
            EventoViewModels eventoViewModel = new EventoViewModels();
            eventoViewModel.DataEvento = evento.DataEvento;
            eventoViewModel.Descricao = evento.Descricao;
            eventoViewModel.Local = evento.Local;
            eventoViewModel.Nome = evento.Nome;
            eventoViewModel.ContatosId = contatos;
            eventoViewModel.Periodicidade = evento.Periodicidade;
            eventoViewModel.Horario = evento.Horario;
            List<UsuarioPassivoViewModels> usuariosPassivos = models.listarContatosDoUsuario();
            eventoViewModel.UsuariosPassivos = usuariosPassivos;

            return View(eventoViewModel);
        }

        // POST: Evento/Editar
        [HttpPost]
        public ActionResult Editar(EventoViewModels eventoViewModel, int[] contatos)
        {
            if (Session["UsuarioAtivoId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                Evento evento = new Evento();
                evento.Id = eventoViewModel.Id;
                evento.Nome = eventoViewModel.Nome;
                evento.DataEvento = eventoViewModel.DataEvento;
                evento.DiaDaSemana = eventoViewModel.DataEvento.DayOfWeek;
                evento.Descricao = eventoViewModel.Descricao;
                evento.Local = eventoViewModel.Local;
                evento.Periodicidade = eventoViewModel.Periodicidade;
                evento.Horario = eventoViewModel.Horario;
                evento.UsuarioAtivo = models.retornarUsuarioLogado();
                if (contatos != null)
                {
                    var usuariosPassivos = new List<UsuarioPassivo>();
                    List<string> contatosEmail = new List<string>();
                    int i = 0;

                    foreach (var contatoId in contatos)
                    {
                        UsuarioPassivo contato = models.consultaUsuariosPassivosPorId(contatoId);
                        contatosEmail.Add(contato.Email);
                        usuariosPassivos.Add(contato);
                    }
                    evento.UsuariosPassivos = usuariosPassivos;

                    foreach (var email in contatosEmail)
                    {
                        var usuarioNome = evento.UsuarioAtivo.Nome + " " + evento.UsuarioAtivo.Sobrenome;
                        var usuarioEmail = evento.UsuarioAtivo.Email;
                        string body = @"<html><body>
                                            <p>Olá! <br/><br/>" + usuarioNome + " remarcou " + evento.Nome + " com você no(a) " + evento.Local +
                                                           " para o dia " + evento.DataEvento.ToShortDateString() + " às " + evento.Horario.ToString(@"hh\:mm") +
                                            ".</p><p>" + evento.Descricao + "<br/> </p> <p>Atenciosamente,</p></body></html>";

                        MailMessage mail = new MailMessage(usuarioEmail, email, evento.Nome, body);
                        mail.From = new MailAddress(usuarioEmail, usuarioNome);
                        mail.IsBodyHtml = true; // necessary if you're using html email

                        NetworkCredential credential = new NetworkCredential("mazulapp@gmail.com", "Asdzxc123$");
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = credential;
                        smtp.Send(mail);
                        i++;
                    }
                }

                models.editarEvento(evento);
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Evento/Visualizar
        [HttpGet]
        public ActionResult Visualizar(int? id)
        {
            if (Session["UsuarioAtivoId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }


            Evento evento = models.consultarEventosPorId((int)id);
            return View(evento);
        }

        // GET: Evento/Excluir
        [HttpGet]
        public ActionResult Excluir(string id)
        {
            if (Session["UsuarioAtivoId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            List<int> eventoIds = new List<int>();
            List<Evento> eventos = new List<Evento>();
            foreach (var item in id.Split(','))
            {
                eventoIds.Add(Convert.ToInt32(item));
            }

            if (eventoIds.Count > 1)
            {
                foreach (var eventoId in eventoIds)
                {
                    Evento evento = models.consultarEventosPorId((int)eventoId);
                    eventos.Add(evento);
                }
            }
            else
            {
                Evento evento = models.consultarEventosPorId((int)eventoIds[0]);
                eventos.Add(evento);
            }
            return View(eventos);
        }

        [HttpGet]
        public JsonResult RecuperarDados(int id)
        {
            try
            {
                var evento = models.consultarEventosPorId(id);

                EventoViewModels eventoViewModel = new EventoViewModels();
                eventoViewModel.Nome = evento.Nome;
                eventoViewModel.DataEvento = evento.DataEvento;
                eventoViewModel.Descricao = evento.Descricao;
                eventoViewModel.Periodicidade = evento.Periodicidade;
                List<string> listaDeContatos = new List<string>();
                foreach (var usuarioPassivo in evento.UsuariosPassivos)
                {
                    string contato = usuarioPassivo.Nome + " " + usuarioPassivo.Sobrenome;
                    listaDeContatos.Add(contato);
                }
                eventoViewModel.ContatosNome = listaDeContatos;
                return Json(eventoViewModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Erro de comunicação com o banco de dados. - " + ex.Message);
            }
        }

        public JsonResult DeletarEventos(string ids)
        {
            try
            {
                List<int> eventoIds = new List<int>();
                foreach (var item in ids.Split(','))
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        eventoIds.Add(Convert.ToInt32(item));
                    }
                }
                if (eventoIds.Count > 1)
                {
                    foreach (var eventoId in eventoIds)
                    {
                        Evento evento = models.consultarEventosPorId((int)eventoId);
                        if (ModelState.IsValid)
                        {
                            models.excluirEvento(evento);
                        }
                    }
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Evento evento = models.consultarEventosPorId((int)eventoIds[0]);
                    if (ModelState.IsValid)
                    {
                        models.excluirEvento(evento);
                        return Json("", JsonRequestBehavior.AllowGet);
                    }
                }
                return Json("Erro");
            }
            catch (Exception ex)
            {
                return Json("Erro de comunicação com o banco de dados. - " + ex.Message);
            }
        }

        public EventoViewModels CarregarListas()
        {
            var eventosViewModel = new EventoViewModels();

            var eventosDiarios = models.consultarEventosDiarios();
            var eventosDomingo = models.consultarDomingos();
            var eventosSegunda = models.consultarSegundas();
            var eventosTerca = models.consultarTercas();
            var eventosQuarta = models.consultarQuartas();
            var eventosQuinta = models.consultarQuintas();
            var eventosSexta = models.consultarSextas();
            var eventosSabado = models.consultarSabados();
            var eventosMensais = models.consultarEventosMensais();
            var eventosPrimeiroDomingo = models.consultarPrimeiroDomingo();
            var eventosSegundoDomingo = models.consultarSegundoDomingo();
            var eventosTerceiroDomingo = models.consultarTerceiroDomingo();
            var eventosQuartoDomingo = models.consultarQuartoDomingo();
            var eventosQuintoDomingo = models.consultarQuintoDomingo();

            eventosViewModel.EventosDiarios = eventosDiarios;
            eventosViewModel.EventosDomingo = eventosDomingo;
            eventosViewModel.EventosSegunda = eventosSegunda;
            eventosViewModel.EventosTerca = eventosTerca;
            eventosViewModel.EventosQuarta = eventosQuarta;
            eventosViewModel.EventosQuinta = eventosQuinta;
            eventosViewModel.EventosSexta = eventosSexta;
            eventosViewModel.EventosSabado = eventosSabado;
            eventosViewModel.EventosMensais = eventosMensais;
            eventosViewModel.EventosDomingo = eventosDomingo;
            eventosViewModel.EventosPrimeiroDomingo = eventosPrimeiroDomingo;
            eventosViewModel.EventosSegundoDomingo = eventosSegundoDomingo;
            eventosViewModel.EventosTerceiroDomingo = eventosTerceiroDomingo;
            eventosViewModel.EventosQuartoDomingo = eventosQuartoDomingo;
            eventosViewModel.EventosQuintoDomingo = eventosQuintoDomingo;

            return eventosViewModel;
        }
    }
}