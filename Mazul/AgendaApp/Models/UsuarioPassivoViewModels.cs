using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AgendaApp.Entidades;

namespace AgendaApp.Models
{
    public class UsuarioPassivoViewModels
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Endereco { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        [Required]
        public string Sexo { get; set; }

        public string NomeUsuarioAtivo { get; set; }

        public ICollection<Evento> Eventos { get; set; }

        public UsuarioAtivo UsuarioAtivo { get; set; }
    }
    
}