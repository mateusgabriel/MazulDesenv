using AgendaApp.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AgendaApp.Models
{
    public class EventoViewModels
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string Periodicidade { get; set; }

        public DateTime DataEvento { get; set; }

        public TimeSpan Horario { get; set; }

        public string Local { get; set; }

        public UsuarioAtivo UsuarioAtivo { get; set; }

        public List<int> ContatosId { get; set; }

        public List<string> ContatosNome { get; set; }

        public ICollection<UsuarioPassivoViewModels> UsuariosPassivos { get; set; }

        public ICollection<Evento> EventosDiarios { get; set; }

        public ICollection<Evento> EventosDomingo { get; set; }

        public ICollection<Evento> EventosSegunda { get; set; }

        public ICollection<Evento> EventosTerca { get; set; }

        public ICollection<Evento> EventosQuarta { get; set; }

        public ICollection<Evento> EventosQuinta { get; set; }

        public ICollection<Evento> EventosSexta { get; set; }

        public ICollection<Evento> EventosSabado { get; set; }

        public ICollection<Evento> EventosMensais { get; set; }

        public ICollection<Evento> EventosPrimeiroDomingo { get; set; }

        public ICollection<Evento> EventosSegundoDomingo { get; set; }

        public ICollection<Evento> EventosTerceiroDomingo { get; set; }

        public ICollection<Evento> EventosQuartoDomingo { get; set; }

        public ICollection<Evento> EventosQuintoDomingo { get; set; }

        public int SemanaDoMes { get; set; }

        private List<string> _itens;

        public List<string> Itens
        {
            get
            {
                _itens = new List<string>();
                _itens.Add("Diária");
                _itens.Add("Semanal");
                _itens.Add("Mensal");
                _itens.Add("Anual");
                return _itens;
            }
        }
    }
}
