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
        private AgendaAppContext db = new AgendaAppContext();

        public UsuarioAtivo VerificarUsuario(string email, string senha)
        {
            //try
            //{
                UsuarioAtivo us = db.UsuariosAtivos.SingleOrDefault(a => a.Email == email);

                if (us != null)
                {
                    if (us.Senha == Hash.CriarSenhaHash(senha, us.Salt))
                    {
                        return us;
                    }
                    else return null;
                }
                else return null;
            //}
            //catch(Exception ex)
            //{
            //    //
            //}
        }
    }
}