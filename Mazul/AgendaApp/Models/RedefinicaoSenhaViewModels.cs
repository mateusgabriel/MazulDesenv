using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgendaApp.Models
{
    public class RedefinicaoSenhaViewModels
    {
        public int IdUsuarioAtivo { get; set;  }

        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "As senhas devem ser iguais.")]
        public string ConfirmaSenha { get; set; }
    }
}