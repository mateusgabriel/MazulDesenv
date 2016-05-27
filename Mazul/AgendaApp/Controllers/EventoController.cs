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
            //MUDE AQUI O ANO PARA TESTAR A CLASSE FERIADOS MÓVEIS
            //int anoTeste = 2016;

            //Feriados fm = new Feriados(anoTeste);
            //List<Feriado> lista = fm.feriados;

            //Console.WriteLine("Feriados do ano: " + anoTeste);
            //Console.WriteLine("------------------------");

            //foreach (Feriado f in lista)
            //    Console.WriteLine(string.Format("{0} - {1}", f.Data.ToString("dd/MM/yyyy"), f.Descricao));

            //Console.WriteLine("------------------------");
            //Console.WriteLine();

            //DateTime dataTeste = DateTime.Parse("2011-03-05");
            //Console.WriteLine("Data de teste: " + dataTeste.ToLongDateString());
            //Console.WriteLine("------------------------");
            //Console.WriteLine("Feriado? " + fm.IsFeriado(dataTeste));
            //Console.WriteLine("Dia útil? " + fm.IsDiaUtil(dataTeste));
            //Console.WriteLine("Próximo dia útil: " + fm.ProximoDiaUtil(dataTeste).ToLongDateString());
            //Console.In.ReadLine();

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
                evento.SemanaDoMes = models.consultaSemanaDoMes(eventoViewModel.DataEvento.Date);
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
                                            <p>Olá! O <br/><br/>" + usuarioNome + " marcou " + evento.Nome + " com você no(a) " + evento.Local +
                                                           " no dia " + evento.DataEvento.ToShortDateString() + " às " + evento.Horario.ToString(@"hh\:mm") +
                                            ".</p><p>" + evento.Descricao + "</p> <p>Atenciosamente, Mazul.</p></body></html>";

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
                            TempData["ErrorMail"] = "Vixe! Parece que houve um problema e não conseguimos enviar para seu e-mail o link de recuperação de senha. >< \n Mas fique tranquilo, já estamos solucionando o problema. o/";
                            //ModelState.AddModelError("ErrorMail", "Ocorreu um erro ao enviar email para contato. Fique tranquilo, já estamos solucionando o problema." + e);
                        }

                    }
                }

                TempData["Sucesso"] = "Pronto! O evento foi criado. ^^";
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
                evento.SemanaDoMes = models.consultaSemanaDoMes(eventoViewModel.DataEvento.Date);
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
                                            <p>Olá! O <br/><br/>" + usuarioNome + " remarcou " + evento.Nome + " com você no(a) " + evento.Local +
                                                           " para o dia " + evento.DataEvento.ToShortDateString() + " às " + evento.Horario.ToString(@"hh\:mm") +
                                            ".</p><p>" + evento.Descricao + "<br/> </p> <p>Atenciosamente, Mazul.</p></body></html>";

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

                TempData["Sucesso"] = "Pronto! O evento foi atualizado. ^^";
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

            TempData["Sucesso"] = "Pronto! O evento foi excluído. ^^";
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

            CarregarListaEventosDiarios(eventosViewModel);
            CarregarListaEventosMensais(eventosViewModel);
            CarregarListaQuantidadeEventos(eventosViewModel);

            return eventosViewModel;
        }

        public EventoViewModels CarregarListaEventosDiarios(EventoViewModels eventosDiariosViewModel)
        {
            var eventosDiarios = models.consultarEventosDiarios();
            var eventosDomingo = models.consultarDomingos();
            var eventosSegunda = models.consultarSegundas();
            var eventosTerca = models.consultarTercas();
            var eventosQuarta = models.consultarQuartas();
            var eventosQuinta = models.consultarQuintas();
            var eventosSexta = models.consultarSextas();
            var eventosSabado = models.consultarSabados();

            eventosDiariosViewModel.EventosDiarios = eventosDiarios;
            eventosDiariosViewModel.EventosDomingo = eventosDomingo;
            eventosDiariosViewModel.EventosSegunda = eventosSegunda;
            eventosDiariosViewModel.EventosTerca = eventosTerca;
            eventosDiariosViewModel.EventosQuarta = eventosQuarta;
            eventosDiariosViewModel.EventosQuinta = eventosQuinta;
            eventosDiariosViewModel.EventosSexta = eventosSexta;
            eventosDiariosViewModel.EventosSabado = eventosSabado;
            eventosDiariosViewModel.EventosDomingo = eventosDomingo;

            return eventosDiariosViewModel;
        }

        public EventoViewModels CarregarListaEventosMensais(EventoViewModels eventosMensaisViewModel)
        {
            var eventosMensais = models.consultarEventosMensais();
            var eventosPrimeiroDomingo = models.consultarPrimeiroDomingo();
            var eventosSegundoDomingo = models.consultarSegundoDomingo();
            var eventosTerceiroDomingo = models.consultarTerceiroDomingo();
            var eventosQuartoDomingo = models.consultarQuartoDomingo();
            var eventosQuintoDomingo = models.consultarQuintoDomingo();

            var eventosPrimeiraSegunda = models.consultarPrimeiraSegunda();
            var eventosSegundaSegunda = models.consultarSegundaSegunda();
            var eventosTerceiraSegunda = models.consultarTerceiraSegunda();
            var eventosQuartaSegunda = models.consultarQuartaSegunda();
            var eventosQuintaSegunda = models.consultarQuintaSegunda();

            var eventosPrimeiraTerca = models.consultarPrimeiraTerca();
            var eventosSegundaTerca= models.consultarSegundaTerca();
            var eventosTerceiraTerca = models.consultarTerceiraTerca();
            var eventosQuartaTerca = models.consultarQuartaTerca();
            var eventosQuintaTerca = models.consultarQuintaTerca();

            var eventosPrimeiraQuarta = models.consultarPrimeiraQuarta();
            var eventosSegundaQuarta = models.consultarSegundaQuarta();
            var eventosTerceiraQuarta = models.consultarTerceiraQuarta();
            var eventosQuartaQuarta = models.consultarQuartaQuarta();
            var eventosQuintaQuarta = models.consultarQuintaQuarta();

            var eventosPrimeiraQuinta = models.consultarPrimeiraQuinta();
            var eventosSegundaQuinta = models.consultarSegundaQuinta();
            var eventosTerceiraQuinta = models.consultarTerceiraQuinta();
            var eventosQuartaQuinta = models.consultarQuartaQuinta();
            var eventosQuintaQuinta = models.consultarQuintaQuinta();

            var eventosPrimeiraSexta = models.consultarPrimeiraSexta();
            var eventosSegundaSexta = models.consultarSegundaSexta();
            var eventosTerceiraSexta = models.consultarTerceiraSexta();
            var eventosQuartaSexta = models.consultarQuartaSexta();
            var eventosQuintaSexta = models.consultarQuintaSexta();

            var eventosPrimeiroSabado = models.consultarPrimeiroSabado();
            var eventosSegundoSabado = models.consultarSegundoSabado();
            var eventosTerceiroSabado = models.consultarTerceiroSabado();
            var eventosQuartoSabado = models.consultarQuartoSabado();
            var eventosQuintoSabado = models.consultarQuintoSabado();

            eventosMensaisViewModel.EventosMensais = eventosMensais;
            eventosMensaisViewModel.EventosPrimeiroDomingo = eventosPrimeiroDomingo;
            eventosMensaisViewModel.EventosSegundoDomingo = eventosSegundoDomingo;
            eventosMensaisViewModel.EventosTerceiroDomingo = eventosTerceiroDomingo;
            eventosMensaisViewModel.EventosQuartoDomingo = eventosQuartoDomingo;
            eventosMensaisViewModel.EventosQuintoDomingo = eventosQuintoDomingo;

            eventosMensaisViewModel.EventosPrimeiraSegunda = eventosPrimeiraSegunda;
            eventosMensaisViewModel.EventosSegundaSegunda = eventosSegundaSegunda;
            eventosMensaisViewModel.EventosTerceiraSegunda = eventosTerceiraSegunda;
            eventosMensaisViewModel.EventosQuartaSegunda = eventosQuartaSegunda;
            eventosMensaisViewModel.EventosQuintaSegunda = eventosQuintaSegunda;

            eventosMensaisViewModel.EventosPrimeiraTerca = eventosPrimeiraTerca;
            eventosMensaisViewModel.EventosSegundaTerca = eventosSegundaTerca;
            eventosMensaisViewModel.EventosTerceiraTerca = eventosTerceiraTerca;
            eventosMensaisViewModel.EventosQuartaTerca = eventosQuartaTerca;
            eventosMensaisViewModel.EventosQuintaTerca = eventosQuintaTerca;

            eventosMensaisViewModel.EventosPrimeiraQuarta = eventosPrimeiraQuarta;
            eventosMensaisViewModel.EventosSegundaQuarta = eventosSegundaQuarta;
            eventosMensaisViewModel.EventosTerceiraQuarta = eventosTerceiraQuarta;
            eventosMensaisViewModel.EventosQuartaQuarta = eventosQuartaQuarta;
            eventosMensaisViewModel.EventosQuintaQuarta = eventosQuintaQuarta;

            eventosMensaisViewModel.EventosPrimeiraQuinta = eventosPrimeiraQuinta;
            eventosMensaisViewModel.EventosSegundaQuinta = eventosSegundaQuinta;
            eventosMensaisViewModel.EventosTerceiraQuinta = eventosTerceiraQuinta;
            eventosMensaisViewModel.EventosQuartaQuinta = eventosQuartaQuinta;
            eventosMensaisViewModel.EventosQuintaQuinta = eventosQuintaQuinta;

            eventosMensaisViewModel.EventosPrimeiraSexta = eventosPrimeiraSexta;
            eventosMensaisViewModel.EventosSegundaSexta = eventosSegundaSexta;
            eventosMensaisViewModel.EventosTerceiraSexta = eventosTerceiraSexta;
            eventosMensaisViewModel.EventosQuartaSexta = eventosQuartaSexta;
            eventosMensaisViewModel.EventosQuintaSexta = eventosQuintaSexta;

            eventosMensaisViewModel.EventosPrimeiroSabado = eventosPrimeiroSabado;
            eventosMensaisViewModel.EventosSegundoSabado = eventosSegundoSabado;
            eventosMensaisViewModel.EventosTerceiroSabado = eventosTerceiroSabado;
            eventosMensaisViewModel.EventosQuartoSabado = eventosQuartoSabado;
            eventosMensaisViewModel.EventosQuintoSabado = eventosQuintoSabado;

            Feriados fm = new Feriados(DateTime.Now.Year);
            eventosMensaisViewModel.Feriados = fm.feriados;

            //foreach (Feriado f in lista)
                //Console.WriteLine(string.Format("{0} - {1}", f.Data.ToString("dd/MM/yyyy"), f.Descricao));

            return eventosMensaisViewModel;
        }

        public EventoViewModels CarregarListaQuantidadeEventos(EventoViewModels quantidadeEventosMensaisViewModel)
        {
            var qtdDomingosMes = models.consultarQtdDomigosNoMes();
            var qtdSegundasMes = models.consultarQtdSegundasNoMes();
            var qtdTercasMes = models.consultarQtdTercasNoMes();
            var qtdQuartasMes = models.consultarQtdQuartasNoMes();
            var qtdQuintasMes = models.consultarQtdQuintasNoMes();
            var qtdSextasMes = models.consultarQtdSextasNoMes();
            var qtdSabadosMes = models.consultarQtdSabadosNoMes();

            quantidadeEventosMensaisViewModel.QuantidadeDomingosMes = qtdDomingosMes;
            quantidadeEventosMensaisViewModel.QuantidadeSegundasMes = qtdSegundasMes;
            quantidadeEventosMensaisViewModel.QuantidadeTercasMes = qtdTercasMes;
            quantidadeEventosMensaisViewModel.QuantidadeQuartasMes = qtdQuartasMes;
            quantidadeEventosMensaisViewModel.QuantidadeQuintasMes = qtdQuintasMes;
            quantidadeEventosMensaisViewModel.QuantidadeSextasMes = qtdSextasMes;
            quantidadeEventosMensaisViewModel.QuantidadeSabadosMes = qtdSabadosMes;

            return quantidadeEventosMensaisViewModel;
        }

    }
}