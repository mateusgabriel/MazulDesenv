using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgendaApp.Entidades
{
    public class UsuarioPassivo
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Endereco { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public int? Sexo { get; set; }

        public virtual ICollection<Evento> Eventos { get; set; }

        public virtual UsuarioAtivo UsuarioAtivo { get; set; }
    }
}