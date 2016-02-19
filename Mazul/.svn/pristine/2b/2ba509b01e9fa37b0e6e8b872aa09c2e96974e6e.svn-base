using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace AgendaApp.Security
{
    public class Hash
    {
        public static string Encode(string value)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }

        public static string CriarSenhaHash(string senha, string salt)
        {
            var saltedSenha = String.Concat(senha, salt);
            string hashedSenha = FormsAuthentication.HashPasswordForStoringInConfigFile(saltedSenha, "sha1");

            return hashedSenha;
        }
    }
}