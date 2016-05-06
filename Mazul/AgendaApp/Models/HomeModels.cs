using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgendaApp.Entidades;
using AgendaApp.Security;

namespace AgendaApp.Models
{
    public class HomeModels
    {
        private MazulContext db = new MazulContext();

        public UsuarioAtivo VerificarUsuario(string email, string senha)
        {
            var usuarioAtivo = new UsuarioAtivo();

            try
            {
                usuarioAtivo = db.UsuariosAtivos.Where(a => a.Email == email).FirstOrDefault();

                if (usuarioAtivo != null)
                {
                    if (usuarioAtivo.Senha == Hash.CriarSenhaHash(senha, usuarioAtivo.Salt))
                    {
                        return usuarioAtivo;
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            return usuarioAtivo;
        }
    }
}