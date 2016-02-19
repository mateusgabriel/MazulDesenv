using AgendaApp.Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AgendaApp.Models
{
    public class UsuarioAtivoViewModels
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Endereco { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        [Compare("Senha",  ErrorMessage = "As senhas devem ser iguais.")]
        public string ConfirmaSenha { get; set; }

        public string Salt { get; set; }

        public DateTime DataRegistro { get; set; }

        [Required]
        public string Sexo { get; set; }

        public virtual ICollection<UsuarioPassivo> UsuariosPassivos { get; set; }
        public virtual ICollection<Evento> Eventos { get; set; }

        public UsuarioAtivoViewModels()
        {
            DataRegistro = DateTime.Now;
        }
    }
}
