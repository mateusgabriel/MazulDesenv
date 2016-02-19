using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgendaApp.Entidades
{
    public class Evento
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }
        
        public string Periodicidade { get; set; }

        public DateTime DataEvento { get; set; }

        public TimeSpan Horario { get; set; }

        public DayOfWeek DiaDaSemana { get; set; }
        
        public string Local { get; set; }

        public virtual UsuarioAtivo UsuarioAtivo { get; set; }

        public virtual ICollection<UsuarioPassivo> UsuariosPassivos { get; set; }
    }
}