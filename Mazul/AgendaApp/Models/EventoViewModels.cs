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

        public List<Evento> EventosDiarios { get; set; }

        public List<Evento> EventosDomingo { get; set; }

        public List<Evento> EventosSegunda { get; set; }

        public List<Evento> EventosTerca { get; set; }

        public List<Evento> EventosQuarta { get; set; }

        public List<Evento> EventosQuinta { get; set; }

        public List<Evento> EventosSexta { get; set; }

        public List<Evento> EventosSabado { get; set; }

        public List<Evento> EventosMensais { get; set; }

        public List<Evento> EventosPrimeiroDomingo { get; set; }

        public List<Evento> EventosSegundoDomingo { get; set; }

        public List<Evento> EventosTerceiroDomingo { get; set; }

        public List<Evento> EventosQuartoDomingo { get; set; }

        public List<Evento> EventosQuintoDomingo { get; set; }

        public List<Evento> EventosPrimeiraSegunda { get; set; }

        public List<Evento> EventosSegundaSegunda { get; set; }

        public List<Evento> EventosTerceiraSegunda { get; set; }

        public List<Evento> EventosQuartaSegunda { get; set; }

        public List<Evento> EventosQuintaSegunda { get; set; }

        public int SemanaDoMes { get; set; }

        public List<DateTime> QuantidadeDomingosMes { get; set; }

        public List<DateTime> QuantidadeSegundasMes { get; set; }

        public List<DateTime> QuantidadeTercasMes { get; set; }

        public List<DateTime> QuantidadeQuartasMes { get; set; }

        public List<DateTime> QuantidadeQuintasMes { get; set; }

        public List<DateTime> QuantidadeSextasMes { get; set; }

        public List<DateTime> QuantidadeSabadosMes { get; set; }



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
