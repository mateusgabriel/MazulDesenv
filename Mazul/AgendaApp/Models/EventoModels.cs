using AgendaApp.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AgendaApp.Models
{
    public class EventoModels
    {

        private MazulContext db = new MazulContext();

        public IList<Evento> consultarEventos()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            List<Evento> eventos = db.Eventos.Where(a => a.UsuarioAtivo.Id == userId).ToList();

            return eventos;
        }

        public IList<Evento> consultarEventosDiarios()
        {
            var date = DateTime.Now.Date;
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            List<Evento> eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId && a.DataEvento == date).ToList();

            return eventos;
        }

        //Consulta todos as datas de domingo do mês e usa apenas a data do primeiro domingo
        public IList<Evento> consultarPrimeiroDomingo()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Sunday);
            DateTime primeiroDomingo = dias[0];

            List<Evento> eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId)
                .Where(a => a.DiaDaSemana == DayOfWeek.Sunday)
                .Where(a => a.DataEvento == primeiroDomingo).ToList();

            return eventos;
        }
        public IList<Evento> consultarSegundoDomingo()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Sunday);
            DateTime primeiroDomingo = dias[1];

            List<Evento> eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId)
                .Where(a => a.DiaDaSemana == DayOfWeek.Sunday)
                .Where(a => a.DataEvento == primeiroDomingo).ToList();

            return eventos;
        }
        public IList<Evento> consultarTerceiroDomingo()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Sunday);
            DateTime primeiroDomingo = dias[2];

            List<Evento> eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId)
                .Where(a => a.DiaDaSemana == DayOfWeek.Sunday)
                .Where(a => a.DataEvento == primeiroDomingo).ToList();

            return eventos;
        }
        public IList<Evento> consultarQuartoDomingo()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Sunday);
            DateTime primeiroDomingo = dias[3];

            List<Evento> eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId)
                .Where(a => a.DiaDaSemana == DayOfWeek.Sunday)
                .Where(a => a.DataEvento == primeiroDomingo).ToList();

            return eventos;
        }


        public List<DateTime> consultarDiaNasSemanasDoMes(int mes, int ano, DayOfWeek dia)
        {
            List<DateTime> diaNasSemanasDoMes = new List<DateTime>();
            DateTime primeiroDiadoMes = new DateTime(ano, mes, 1);
            DateTime diaAtual = primeiroDiadoMes;

            while (primeiroDiadoMes.Month == diaAtual.Month)
            {
                DayOfWeek diaNaSemana = diaAtual.DayOfWeek;
                if (diaNaSemana == dia)
                {
                    diaNasSemanasDoMes.Add(diaAtual);
                }

                diaAtual = diaAtual.AddDays(1);
            }

            return diaNasSemanasDoMes;
        }

        public IList<Evento> consultarSegundas()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var semanaDoMes = consultaSemanaDoMes(DateTime.Today);

            List<Evento> eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId &&
                        a.DiaDaSemana == DayOfWeek.Monday &&
                            a.SemanaDoMes == semanaDoMes).ToList();

            return eventos;
        }

        public IList<Evento> consultarTercas()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var semanaDoMes = consultaSemanaDoMes(DateTime.Today);

            List<Evento> eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId &&
                        a.DiaDaSemana == DayOfWeek.Tuesday &&
                            a.SemanaDoMes == semanaDoMes).ToList();

            return eventos;
        }

        public IList<Evento> consultarQuartas()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var semanaDoMes = consultaSemanaDoMes(DateTime.Today);

            List<Evento> eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId &&
                        a.DiaDaSemana == DayOfWeek.Wednesday &&
                            a.SemanaDoMes == semanaDoMes).ToList();

            return eventos;
        }

        public IList<Evento> consultarQuintas()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var semanaDoMes = consultaSemanaDoMes(DateTime.Today);

            List<Evento> eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId &&
                        a.DiaDaSemana == DayOfWeek.Thursday &&
                            a.SemanaDoMes == semanaDoMes).ToList();

            return eventos;
        }

        public IList<Evento> consultarSextas()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var semanaDoMes = consultaSemanaDoMes(DateTime.Today);

            List<Evento> eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId && 
                        a.DiaDaSemana == DayOfWeek.Friday &&
                            a.SemanaDoMes == semanaDoMes).ToList();

            return eventos;
        }

        public IList<Evento> consultarSabados()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var semanaDoMes = consultaSemanaDoMes(DateTime.Today);

            List<Evento> eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId &&
                        a.DiaDaSemana == DayOfWeek.Saturday &&
                            a.SemanaDoMes == semanaDoMes).ToList();

            return eventos;
        }

        public IList<Evento> consultarDomingos()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var semanaDoMes = consultaSemanaDoMes(DateTime.Today);

            List<Evento> eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId &&
                        a.DiaDaSemana == DayOfWeek.Sunday &&
                            a.SemanaDoMes == semanaDoMes).ToList();

            return eventos;
        }

        public IList<Evento> consultarEventosMensais()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            List<Evento> eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId).ToList();

            return eventos;
        }

        public Evento consultarEventosPorId(int id)
        {
            return db.Eventos.Find(id);
        }

        public void inserirEvento(Evento evento)
        {
            try
            {
                db.Eventos.Add(evento);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                //
            }
        }

        public void editarEvento(Evento evento)
        {
            try
            {
                Evento evnt = db.Eventos.SingleOrDefault(a => a.Id == evento.Id);
                db.Entry(evnt).State = EntityState.Deleted;
                db.SaveChanges();

                db.Eventos.Add(evento);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                //
            }
        }

        public void excluirEvento(Evento evento)
        {
            try
            {
                db.Entry(evento).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                //
            }
        }

        public List<UsuarioPassivoViewModels> listarContatosDoUsuario()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            List<UsuarioPassivo> usuariosPassivos = db.UsuariosPassivos.Where(a => a.UsuarioAtivo.Id == userId).ToList();
            var usuariospassivosViewModel = new List<UsuarioPassivoViewModels>();
            foreach (var usuarioPassivo in usuariosPassivos)
            {
                var upViewModel = new UsuarioPassivoViewModels();
                upViewModel.Id = usuarioPassivo.Id;
                upViewModel.Nome = usuarioPassivo.Nome;
                upViewModel.Sobrenome = usuarioPassivo.Sobrenome;
                upViewModel.Telefone = usuarioPassivo.Telefone;
                upViewModel.Email = usuarioPassivo.Email;
                upViewModel.Endereco = usuarioPassivo.Endereco;
                if (usuarioPassivo.Sexo == 1)
                {
                    upViewModel.Sexo = "Masculino";
                }
                else
                {
                    upViewModel.Sexo = "Feminino";
                }
                usuariospassivosViewModel.Add(upViewModel);
            }

            return usuariospassivosViewModel;
        }

        public UsuarioPassivo consultaUsuariosPassivosPorId(int id)
        {
            return db.UsuariosPassivos.Find(id);
        }

        public UsuarioAtivo retornarUsuarioLogado()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            UsuarioAtivo us = db.UsuariosAtivos.SingleOrDefault(a => a.Id == userId);
            return us;
        }

        public int consultaSemanaDoMes(DateTime TargetDate)
        {
            return (TargetDate.Day - 1) / 7 + 1;
        }
    }
}