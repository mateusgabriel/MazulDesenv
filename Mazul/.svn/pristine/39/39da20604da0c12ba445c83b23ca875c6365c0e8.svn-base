using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaApp.Entidades
{
    public class UsuarioAtivo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Salt { get; set; }
        public DateTime DataRegistro { get; set; }
        public int Sexo { get; set; }

        public virtual ICollection<UsuarioPassivo> UsuariosPassivos { get; set; }
        public virtual ICollection<Evento> Eventos { get; set; }

        public UsuarioAtivo()
        {
            DataRegistro = DateTime.Now;
        }
    }
}
