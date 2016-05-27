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

        #region Eventos Diários

        public List<Evento> EventosDiarios { get; set; }

        public List<Evento> EventosDomingo { get; set; }

        public List<Evento> EventosSegunda { get; set; }

        public List<Evento> EventosTerca { get; set; }

        public List<Evento> EventosQuarta { get; set; }

        public List<Evento> EventosQuinta { get; set; }

        public List<Evento> EventosSexta { get; set; }

        public List<Evento> EventosSabado { get; set; }

        #endregion

        #region Eventos Mensais

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

        public List<Evento> EventosPrimeiraTerca { get; set; }
        public List<Evento> EventosSegundaTerca { get; set; }
        public List<Evento> EventosTerceiraTerca { get; set; }
        public List<Evento> EventosQuartaTerca { get; set; }
        public List<Evento> EventosQuintaTerca { get; set; }

        public List<Evento> EventosPrimeiraQuarta { get; set; }
        public List<Evento> EventosSegundaQuarta { get; set; }
        public List<Evento> EventosTerceiraQuarta { get; set; }
        public List<Evento> EventosQuartaQuarta { get; set; }
        public List<Evento> EventosQuintaQuarta { get; set; }

        public List<Evento> EventosPrimeiraQuinta { get; set; }
        public List<Evento> EventosSegundaQuinta { get; set; }
        public List<Evento> EventosTerceiraQuinta { get; set; }
        public List<Evento> EventosQuartaQuinta { get; set; }

        public List<Evento> EventosQuintaQuinta { get; set; }
        public List<Evento> EventosPrimeiraSexta { get; set; }
        public List<Evento> EventosSegundaSexta { get; set; }
        public List<Evento> EventosTerceiraSexta { get; set; }
        public List<Evento> EventosQuartaSexta { get; set; }
        public List<Evento> EventosQuintaSexta { get; set; }

        public List<Evento> EventosPrimeiroSabado { get; set; }
        public List<Evento> EventosSegundoSabado { get; set; }
        public List<Evento> EventosTerceiroSabado { get; set; }
        public List<Evento> EventosQuartoSabado { get; set; }
        public List<Evento> EventosQuintoSabado { get; set; }

        #endregion

        #region Quantidade Dias Mensais

        public List<DateTime> QuantidadeDomingosMes { get; set; }

        public List<DateTime> QuantidadeSegundasMes { get; set; }

        public List<DateTime> QuantidadeTercasMes { get; set; }

        public List<DateTime> QuantidadeQuartasMes { get; set; }

        public List<DateTime> QuantidadeQuintasMes { get; set; }

        public List<DateTime> QuantidadeSextasMes { get; set; }

        public List<DateTime> QuantidadeSabadosMes { get; set; }

        #endregion

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

        public List<Feriado> Feriados;
    }
}
