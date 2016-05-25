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
            var eventos = db.Eventos.Where(a => a.UsuarioAtivo.Id == userId).ToList();

            return eventos;
        }

        public List<Evento> consultarEventosDiarios()
        {
            var date = DateTime.Now.Date;
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId && a.DataEvento == date).ToList();

            return eventos;
        }

        public List<DateTime> consultarQtdDomigosNoMes()
        {
            return consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Sunday);
        }

        public List<DateTime> consultarQtdSegundasNoMes()
        {
            return consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Monday);
        }

        public List<DateTime> consultarQtdTercasNoMes()
        {
            return consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Tuesday);
        }

        public List<DateTime> consultarQtdQuartasNoMes()
        {
            return consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Wednesday);
        }

        public List<DateTime> consultarQtdQuintasNoMes()
        {
            return consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Thursday);
        }

        public List<DateTime> consultarQtdSextasNoMes()
        {
            return consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Friday);
        }

        public List<DateTime> consultarQtdSabadosNoMes()
        {
            return consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Saturday);
        }

        #region domingosDoMês
        //Consulta todos as datas de domingo do mês e usa apenas a data do primeiro domingo
        public List<Evento> consultarPrimeiroDomingo()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Sunday);
            DateTime primeiroDomingo = dias[0];

            var eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId &&
                        a.DiaDaSemana == DayOfWeek.Sunday &&
                            a.DataEvento == primeiroDomingo).ToList();

            return eventos;
        }

        public List<Evento> consultarSegundoDomingo()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Sunday);
            DateTime segundoDomingo = dias[1];

            var eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId &&
                        a.DiaDaSemana == DayOfWeek.Sunday &&
                            a.DataEvento == segundoDomingo).ToList();

            return eventos;
        }

        public List<Evento> consultarTerceiroDomingo()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Sunday);
            DateTime terceiroDomingo = dias[2];

            var eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId &&
                        a.DiaDaSemana == DayOfWeek.Sunday &&
                            a.DataEvento == terceiroDomingo).ToList();

            return eventos;
        }

        public List<Evento> consultarQuartoDomingo()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Sunday);
            DateTime quartoDomingo = dias[3];

            var eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId &&
                        a.DiaDaSemana == DayOfWeek.Sunday &&
                            a.DataEvento == quartoDomingo).ToList();

            return eventos;
        }

        public List<Evento> consultarQuintoDomingo()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Sunday);
            var quintoDomingo = dias.Count() > 4 ? dias[4] : default(DateTime);
            var eventos = new List<Evento>();

            if (quintoDomingo != default(DateTime))
            {
                eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId &&
                        a.DiaDaSemana == DayOfWeek.Sunday &&
                            a.DataEvento == quintoDomingo).ToList();
            }
                

            return eventos;
        }
        #endregion

        #region segundasDoMÊs
            //Consulta todos as datas de domingo do mês e usa apenas a data do primeiro domingo
            public List<Evento> consultarPrimeiraSegunda()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Monday);
                DateTime primeiraSegunda = dias[0];

                var eventos = db.Eventos
                    .Where(a => a.UsuarioAtivo.Id == userId &&
                            a.DiaDaSemana == DayOfWeek.Monday &&
                                a.DataEvento == primeiraSegunda).ToList();

                return eventos;
            }

            public List<Evento> consultarSegundaSegunda()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Monday);
                DateTime segundaSegunda = dias[1];

                var eventos = db.Eventos
                    .Where(a => a.UsuarioAtivo.Id == userId &&
                            a.DiaDaSemana == DayOfWeek.Monday &&
                                a.DataEvento == segundaSegunda).ToList();

                return eventos;
            }

            public List<Evento> consultarTerceiraSegunda()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Monday);
                DateTime terceiraSegunda = dias[2];

                var eventos = db.Eventos
                    .Where(a => a.UsuarioAtivo.Id == userId &&
                            a.DiaDaSemana == DayOfWeek.Monday &&
                                a.DataEvento == terceiraSegunda).ToList();

                return eventos;
            }

            public List<Evento> consultarQuartaSegunda()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Monday);
                DateTime quartaSegunda = dias[3];

                var eventos = db.Eventos
                    .Where(a => a.UsuarioAtivo.Id == userId &&
                            a.DiaDaSemana == DayOfWeek.Monday &&
                                a.DataEvento == quartaSegunda).ToList();

                return eventos;
            }

            public List<Evento> consultarQuintaSegunda()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Monday);
                DateTime quintaSegunda = dias.Count() > 4 ? dias[4] : default(DateTime);
                var eventos = new List<Evento>();

                if (quintaSegunda != default(DateTime)) {
                    eventos = db.Eventos
                        .Where(a => a.UsuarioAtivo.Id == userId &&
                                a.DiaDaSemana == DayOfWeek.Monday &&
                                    a.DataEvento == quintaSegunda).ToList();
                }

                return eventos;
            }
        #endregion

        #region terçasDoMÊs
            public List<Evento> consultarPrimeiraTerca()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Tuesday);
                DateTime primeiraTerca = dias[0];

                var eventos = db.Eventos
                    .Where(a => a.UsuarioAtivo.Id == userId &&
                            a.DiaDaSemana == DayOfWeek.Tuesday &&
                                a.DataEvento == primeiraTerca).ToList();

                return eventos;
            }

            public List<Evento> consultarSegundaTerca()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Tuesday);
                DateTime segundaTerca = dias[1];

                var eventos = db.Eventos
                    .Where(a => a.UsuarioAtivo.Id == userId &&
                            a.DiaDaSemana == DayOfWeek.Tuesday &&
                                a.DataEvento == segundaTerca).ToList();

                return eventos;
            }

            public List<Evento> consultarTerceiraTerca()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Tuesday);
                DateTime terceiraTerca = dias[2];

                var eventos = db.Eventos
                    .Where(a => a.UsuarioAtivo.Id == userId &&
                            a.DiaDaSemana == DayOfWeek.Tuesday &&
                                a.DataEvento == terceiraTerca).ToList();

                return eventos;
            }

            public List<Evento> consultarQuartaTerca()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Tuesday);
                DateTime quartaTerca = dias[3];

                var eventos = db.Eventos
                    .Where(a => a.UsuarioAtivo.Id == userId &&
                            a.DiaDaSemana == DayOfWeek.Tuesday &&
                                a.DataEvento == quartaTerca).ToList();

                return eventos;
            }

            public List<Evento> consultarQuintaTerca()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Tuesday);
                DateTime quintaTerca = dias.Count() > 4 ? dias[4] : default(DateTime);
                var eventos = new List<Evento>();

                if (quintaTerca != default(DateTime))
                {
                     eventos = db.Eventos
                        .Where(a => a.UsuarioAtivo.Id == userId &&
                                a.DiaDaSemana == DayOfWeek.Tuesday &&
                                    a.DataEvento == quintaTerca).ToList();
                }

                return eventos;
            }
        #endregion

        #region quartasDoMÊs
            public List<Evento> consultarPrimeiraQuarta()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Wednesday);
                DateTime primeiraQuarta = dias[0];

                var eventos = db.Eventos
                    .Where(a => a.UsuarioAtivo.Id == userId &&
                            a.DiaDaSemana == DayOfWeek.Wednesday &&
                                a.DataEvento == primeiraQuarta).ToList();

                return eventos;
            }

            public List<Evento> consultarSegundaQuarta()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Wednesday);
                DateTime segundaQuarta = dias[1];

                var eventos = db.Eventos
                    .Where(a => a.UsuarioAtivo.Id == userId &&
                            a.DiaDaSemana == DayOfWeek.Wednesday &&
                                a.DataEvento == segundaQuarta).ToList();

                return eventos;
            }

            public List<Evento> consultarTerceiraQuarta()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Wednesday);
                DateTime terceiraQuarta = dias[2];

                var eventos = db.Eventos
                    .Where(a => a.UsuarioAtivo.Id == userId &&
                            a.DiaDaSemana == DayOfWeek.Wednesday &&
                                a.DataEvento == terceiraQuarta).ToList();

                return eventos;
            }

            public List<Evento> consultarQuartaQuarta()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Wednesday);
                DateTime quartaQuarta = dias[3];

                var eventos = db.Eventos
                    .Where(a => a.UsuarioAtivo.Id == userId &&
                            a.DiaDaSemana == DayOfWeek.Wednesday &&
                                a.DataEvento == quartaQuarta).ToList();

                return eventos;
            }

            public List<Evento> consultarQuintaQuarta()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Wednesday);
                DateTime quintaQuarta = dias.Count() > 4 ? dias[4] : default(DateTime);
                var eventos = new List<Evento>();

                if (quintaQuarta != default(DateTime))
                {
                    eventos = db.Eventos
                        .Where(a => a.UsuarioAtivo.Id == userId &&
                                a.DiaDaSemana == DayOfWeek.Wednesday &&
                                    a.DataEvento == quintaQuarta).ToList();
                }

                return eventos;
            }
        #endregion

        #region quintasDoMÊs
            public List<Evento> consultarPrimeiraQuinta()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Thursday);
                DateTime primeiraQuinta = dias[0];

                var eventos = db.Eventos
                    .Where(a => a.UsuarioAtivo.Id == userId &&
                            a.DiaDaSemana == DayOfWeek.Thursday &&
                                a.DataEvento == primeiraQuinta).ToList();

                return eventos;
            }

            public List<Evento> consultarSegundaQuinta()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Thursday);
                DateTime segundaQuinta = dias[1];

                var eventos = db.Eventos
                    .Where(a => a.UsuarioAtivo.Id == userId &&
                            a.DiaDaSemana == DayOfWeek.Thursday &&
                                a.DataEvento == segundaQuinta).ToList();

                return eventos;
            }

            public List<Evento> consultarTerceiraQuinta()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Thursday);
                DateTime terceiraQuinta = dias[2];

                var eventos = db.Eventos
                    .Where(a => a.UsuarioAtivo.Id == userId &&
                            a.DiaDaSemana == DayOfWeek.Thursday &&
                                a.DataEvento == terceiraQuinta).ToList();

                return eventos;
            }

            public List<Evento> consultarQuartaQuinta()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Thursday);
                DateTime quartaQuinta = dias[3];

                var eventos = db.Eventos
                    .Where(a => a.UsuarioAtivo.Id == userId &&
                            a.DiaDaSemana == DayOfWeek.Thursday &&
                                a.DataEvento == quartaQuinta).ToList();

                return eventos;
            }

            public List<Evento> consultarQuintaQuinta()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Thursday);
                DateTime quintaQuinta = dias.Count() > 4 ? dias[4] : default(DateTime);
                var eventos = new List<Evento>();

                if (quintaQuinta != default(DateTime))
                {
                    eventos = db.Eventos
                        .Where(a => a.UsuarioAtivo.Id == userId &&
                                a.DiaDaSemana == DayOfWeek.Thursday &&
                                    a.DataEvento == quintaQuinta).ToList();
                }

                return eventos;
            }
        #endregion

        #region sextasDoMÊs
        public List<Evento> consultarPrimeiraSexta()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Friday);
            DateTime primeiraSexta = dias[0];

            var eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId &&
                        a.DiaDaSemana == DayOfWeek.Friday &&
                            a.DataEvento == primeiraSexta).ToList();

            return eventos;
        }

        public List<Evento> consultarSegundaSexta()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Friday);
            DateTime segundaSexta = dias[1];

            var eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId &&
                        a.DiaDaSemana == DayOfWeek.Friday &&
                            a.DataEvento == segundaSexta).ToList();

            return eventos;
        }

        public List<Evento> consultarTerceiraSexta()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Friday);
            DateTime terceiraSexta = dias[2];

            var eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId &&
                        a.DiaDaSemana == DayOfWeek.Friday &&
                            a.DataEvento == terceiraSexta).ToList();

            return eventos;
        }

        public List<Evento> consultarQuartaSexta()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Friday);
            DateTime quartaSexta = dias[3];

            var eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId &&
                        a.DiaDaSemana == DayOfWeek.Friday &&
                            a.DataEvento == quartaSexta).ToList();

            return eventos;
        }

        public List<Evento> consultarQuintaSexta()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Friday);
            DateTime quintaSexta = dias.Count() > 4 ? dias[4] : default(DateTime);
            var eventos = new List<Evento>();

            if (quintaSexta != default(DateTime))
            {
                 eventos = db.Eventos
                    .Where(a => a.UsuarioAtivo.Id == userId &&
                            a.DiaDaSemana == DayOfWeek.Friday &&
                                a.DataEvento == quintaSexta).ToList();
            }

            return eventos;
        }
        #endregion

        #region sabadosDoMÊs
        public List<Evento> consultarPrimeiroSabado()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Saturday);
                DateTime primeiroSabado = dias[0];

                var eventos = db.Eventos
                    .Where(a => a.UsuarioAtivo.Id == userId &&
                            a.DiaDaSemana == DayOfWeek.Saturday &&
                                a.DataEvento == primeiroSabado).ToList();

                return eventos;
            }

            public List<Evento> consultarSegundoSabado()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Saturday);
                DateTime segundoSabado = dias[1];

                var eventos = db.Eventos
                    .Where(a => a.UsuarioAtivo.Id == userId &&
                            a.DiaDaSemana == DayOfWeek.Saturday &&
                                a.DataEvento == segundoSabado).ToList();

                return eventos;
            }

            public List<Evento> consultarTerceiroSabado()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Saturday);
                DateTime terceiroSabado = dias[2];

                var eventos = db.Eventos
                    .Where(a => a.UsuarioAtivo.Id == userId &&
                            a.DiaDaSemana == DayOfWeek.Saturday &&
                                a.DataEvento == terceiroSabado).ToList();

                return eventos;
            }

            public List<Evento> consultarQuartoSabado()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Saturday);
                DateTime quartoSabado = dias[3];

                var eventos = db.Eventos
                    .Where(a => a.UsuarioAtivo.Id == userId &&
                            a.DiaDaSemana == DayOfWeek.Saturday &&
                                a.DataEvento == quartoSabado).ToList();

                return eventos;
            }

            public List<Evento> consultarQuintoSabado()
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                var dias = consultarDiaNasSemanasDoMes(DateTime.Now.Month, DateTime.Now.Year, DayOfWeek.Saturday);
                DateTime quintoSabado = dias.Count() > 4 ? dias[4] : default(DateTime);
                var eventos = new List<Evento>();

                if (quintoSabado != default(DateTime))
                {
                    eventos = db.Eventos
                        .Where(a => a.UsuarioAtivo.Id == userId &&
                                a.DiaDaSemana == DayOfWeek.Saturday &&
                                    a.DataEvento == quintoSabado).ToList();
                }

                return eventos;
            }
        #endregion
        
        public List<DateTime> consultarDiaNasSemanasDoMes(int mes, int ano, DayOfWeek dia)
        {
            //mes = 6;
            var diaNasSemanasDoMes = new List<DateTime>();
            var primeiroDiadoMes = new DateTime(ano, mes, 1);
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

        public List<Evento> consultarSegundas()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var semanaDoMes = consultaSemanaDoMes(DateTime.Today);

            var eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId &&
                        a.DiaDaSemana == DayOfWeek.Monday &&
                            a.SemanaDoMes == semanaDoMes).ToList();

            return eventos;
        }

        public List<Evento> consultarTercas()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var semanaDoMes = consultaSemanaDoMes(DateTime.Today);

            var eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId &&
                        a.DiaDaSemana == DayOfWeek.Tuesday &&
                            a.SemanaDoMes == semanaDoMes).ToList();

            return eventos;
        }

        public List<Evento> consultarQuartas()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var semanaDoMes = consultaSemanaDoMes(DateTime.Today);

            var eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId &&
                        a.DiaDaSemana == DayOfWeek.Wednesday &&
                            a.SemanaDoMes == semanaDoMes).ToList();

            return eventos;
        }

        public List<Evento> consultarQuintas()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var semanaDoMes = consultaSemanaDoMes(DateTime.Today);

            var eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId &&
                        a.DiaDaSemana == DayOfWeek.Thursday &&
                            a.SemanaDoMes == semanaDoMes).ToList();

            return eventos;
        }

        public List<Evento> consultarSextas()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var semanaDoMes = consultaSemanaDoMes(DateTime.Today);

            var eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId && 
                        a.DiaDaSemana == DayOfWeek.Friday &&
                            a.SemanaDoMes == semanaDoMes).ToList();

            return eventos;
        }

        public List<Evento> consultarSabados()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var semanaDoMes = consultaSemanaDoMes(DateTime.Today);

            var eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId &&
                        a.DiaDaSemana == DayOfWeek.Saturday &&
                            a.SemanaDoMes == semanaDoMes).ToList();

            return eventos;
        }

        public List<Evento> consultarDomingos()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var semanaDoMes = consultaSemanaDoMes(DateTime.Today);

            var eventos = db.Eventos
                .Where(a => a.UsuarioAtivo.Id == userId &&
                        a.DiaDaSemana == DayOfWeek.Sunday &&
                            a.SemanaDoMes == semanaDoMes).ToList();

            return eventos;
        }

        public List<Evento> consultarEventosMensais()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            var eventos = db.Eventos
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