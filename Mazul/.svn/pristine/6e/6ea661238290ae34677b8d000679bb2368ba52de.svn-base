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

        private AgendaAppContext db = new AgendaAppContext();

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
                .Where(a => a.UsuarioAtivo.Id == userId)
                .Where(a => a.DataEvento == date).ToList();

            return eventos;
        }

        public IList<Evento> consultarDomingos()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);

            List<Evento> eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId)
                .Where(a => a.DiaDaSemana == DayOfWeek.Sunday).ToList();

            return eventos;
        }

        //Consulta todos as datas de domingo do mês de Setembro e usa apenas a data do primeiro domingo
        public IList<Evento> consultarPrimeiroDomingo()
        {
            var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Sunday);
            DateTime primeiroDomingo = dias[0];
            //DateTime teste = GetDates()
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);

            List<Evento> eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId)
                .Where(a => a.DiaDaSemana == DayOfWeek.Sunday)
                .Where(a => a.DataEvento == primeiroDomingo).ToList();
                //.Where(a => a.DataEvento in DateTime.Now.Day)

            return eventos;
        }
        public IList<Evento> consultarSegundoDomingo()
        {
            var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Sunday);
            DateTime primeiroDomingo = dias[1];
            //DateTime teste = GetDates()
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);

            List<Evento> eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId)
                .Where(a => a.DiaDaSemana == DayOfWeek.Sunday)
                .Where(a => a.DataEvento == primeiroDomingo).ToList();
            //.Where(a => a.DataEvento in DateTime.Now.Day)

            return eventos;
        }
        public IList<Evento> consultarTerceiroDomingo()
        {
            var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Sunday);
            DateTime primeiroDomingo = dias[2];
            //DateTime teste = GetDates()
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);

            List<Evento> eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId)
                .Where(a => a.DiaDaSemana == DayOfWeek.Sunday)
                .Where(a => a.DataEvento == primeiroDomingo).ToList();
            //.Where(a => a.DataEvento in DateTime.Now.Day)

            return eventos;
        }
        public IList<Evento> consultarQuartoDomingo()
        {
            var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Sunday);
            DateTime primeiroDomingo = dias[3];
            //DateTime teste = GetDates()
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);

            List<Evento> eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId)
                .Where(a => a.DiaDaSemana == DayOfWeek.Sunday)
                .Where(a => a.DataEvento == primeiroDomingo).ToList();
            //.Where(a => a.DataEvento in DateTime.Now.Day)

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
                    diaNasSemanasDoMes.Add(diaAtual);

                diaAtual = diaAtual.AddDays(1);
            }

            return diaNasSemanasDoMes;
        }

        public IList<Evento> consultarSegundas()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);

            List<Evento> eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId)
                .Where(a => a.DiaDaSemana == DayOfWeek.Monday).ToList();

            return eventos;
        }

        public IList<Evento> consultarTercas()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);

            List<Evento> eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId)
                .Where(a => a.DiaDaSemana == DayOfWeek.Tuesday).ToList();

            return eventos;
        }

        public IList<Evento> consultarQuartas()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);

            List<Evento> eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId)
                .Where(a => a.DiaDaSemana == DayOfWeek.Wednesday).ToList();

            return eventos;
        }

        public IList<Evento> consultarQuintas()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);

            List<Evento> eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId)
                .Where(a => a.DiaDaSemana == DayOfWeek.Thursday).ToList();

            return eventos;
        }

        public IList<Evento> consultarSextas()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);

            List<Evento> eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId)
                .Where(a => a.DiaDaSemana == DayOfWeek.Friday).ToList();

            return eventos;
        }

        public IList<Evento> consultarSabados()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);

            List<Evento> eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId)
                .Where(a => a.DiaDaSemana == DayOfWeek.Saturday).ToList();

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

        

        //public static List<DateTime> GetDates(int year, int month)
        //{
        //    var dates = new List<DateTime>();

        //    // Loop from the first day of the month until we hit the next month, moving forward a day at a time
        //    for (var date = new DateTime(year, month, 1); date.Month == month; date = date.AddDays(1))
        //    {
        //        dates.Add(date);
        //    }

        //    return dates;
        //}
    }
}