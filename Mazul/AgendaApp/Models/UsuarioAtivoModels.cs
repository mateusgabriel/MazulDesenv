﻿using AgendaApp.Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Security;
using AgendaApp.Security;

namespace AgendaApp.Models
{
    public class UsuarioAtivoModels
    {

        private MazulContext db = new MazulContext();

        public IList<UsuarioAtivo> consultarUsuariosAtivos()
        {
            return db.UsuariosAtivos.ToList();
        }

        public UsuarioAtivo consultarUsuariosAtivosPorId(int id)
        {
            return db.UsuariosAtivos.Find(id);
        }

        public bool inserirUsuarioAtivo(UsuarioAtivo usuarioAtivo)
        {
            bool valida = true;
            if (ValidarSenha(usuarioAtivo.Senha))
            {
                var salt = Crypto.GenerateSalt();
                usuarioAtivo.Senha = Hash.CriarSenhaHash(usuarioAtivo.Senha, salt);
                usuarioAtivo.Salt = salt;

                db.UsuariosAtivos.Add(usuarioAtivo);
                db.SaveChanges();
            }
            else
            {
                valida = false;
            }

            return valida;
        }

        public bool editarUsuarioAtivo(UsuarioAtivo usuarioAtivo, string salt)
        {
            bool valida = true;
            try
            {

               if (ValidarSenha(usuarioAtivo.Senha))
                {
                    usuarioAtivo.Senha = Hash.CriarSenhaHash(usuarioAtivo.Senha, salt);
                    usuarioAtivo.Salt = salt;

                    db.Entry(usuarioAtivo).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else {
                    valida = false;
                }             
            }
            catch (Exception ex)
            {
                //
            }

            return valida;
        }

        public bool editarSalt(UsuarioAtivo usuarioAtivo, string salt)
        {
            bool valida = true;
            try
            {
                   usuarioAtivo.Salt = salt;

                    db.Entry(usuarioAtivo).State = EntityState.Modified;
                    db.SaveChanges();
              
            }
            catch (Exception ex)
            {
                //
            }

            return valida;
        }

        public void excluirUsuarioAtivo(UsuarioAtivo usuarioAtivo)
        {
            try
            {
                db.Entry(usuarioAtivo).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                //
            }
        }

        public void excluirTodosUsuariosPassivos(List<UsuarioPassivo> usuariosPassivos)
        {
            try
            {
                foreach (var usuario in usuariosPassivos)
                {
                    db.Entry(usuario).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //
            }
        }

        public bool ValidarSenha(string senha)
        {
            if (senha == null) return false;

            const int MIN_LENGTH = 7;
            const int MAX_LENGTH = 25;
            string specialCharacters = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            char[] specialArray = specialCharacters.ToCharArray();

            bool meetsLengthRequirements = senha.Length >= MIN_LENGTH && senha.Length <= MAX_LENGTH;
            bool hasUpperCaseLetter = false;
            bool hasLowerCaseLetter = false;
            bool hasDecimalDigit = false;
            bool hasSpecialSymbol = senha.IndexOfAny(specialArray) != -1;

            if (meetsLengthRequirements)
            {
                foreach (char c in senha)
                {
                    if (char.IsUpper(c)) hasUpperCaseLetter = true;
                    else if (char.IsLower(c)) hasLowerCaseLetter = true;
                    else if (char.IsDigit(c)) hasDecimalDigit = true;
                }
            }

            bool isValid = meetsLengthRequirements
                        && hasUpperCaseLetter
                        && hasLowerCaseLetter
                        && hasDecimalDigit
                        && hasSpecialSymbol
                        ;
            return isValid;
        }

        public UsuarioAtivo consultarUsuarioAtivoPorLogin(string login)
        {
            var usuarioAtivo = new UsuarioAtivo();

            try
            {
                usuarioAtivo = db.UsuariosAtivos.Where(a => a.Email == login).FirstOrDefault();

                if (usuarioAtivo != null)
                {
                    return usuarioAtivo;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return usuarioAtivo;
        }

        public UsuarioAtivo consultarUsuarioAtivoPorSalt(string salt) 
        {
            var usuarioAtivo = new UsuarioAtivo();

            try
            {
                usuarioAtivo = db.UsuariosAtivos.Where(a => a.Salt == salt).FirstOrDefault();

                if (usuarioAtivo != null)
                {
                    return usuarioAtivo;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return usuarioAtivo;
        }
    }
    
}
