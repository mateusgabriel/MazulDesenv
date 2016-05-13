using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AgendaApp.Entidades;

namespace AgendaApp.Models
{
    public class UsuarioPassivoModels
    {
        private AgendaAppContext db = new AgendaAppContext();

        public IList<UsuarioPassivo> consultaUsuariosPassivos()
        {
            return db.UsuariosPassivos.ToList();
        }

        public UsuarioPassivo consultaUsuariosPassivosPorId(int id)
        {
            return db.UsuariosPassivos.Find(id);
        }

        public List<UsuarioPassivo> listarUsuariosPassivosPorId()
        {
            var userId = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
            List<UsuarioPassivo> up = db.UsuariosPassivos.Where(a => a.UsuarioAtivo.Id == userId).ToList();
            return up;
        }

        public void editarUsuarioPassivo(UsuarioPassivo usuarioPassivo)
        {
            try
            {
                db.Entry(usuarioPassivo).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void excluirUsuarioPassivo(UsuarioPassivo usuarioPassivo)
        {
            try
            {
                db.Entry(usuarioPassivo).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void inserirUsuarioPassivo(UsuarioPassivo usuarioPassivo)
        {
            try
            {
                var id = Convert.ToInt32(HttpContext.Current.Session["UsuarioAtivoId"]);
                UsuarioAtivo us = db.UsuariosAtivos.SingleOrDefault(a => a.Id == id);
                usuarioPassivo.UsuarioAtivo = us;
                db.UsuariosPassivos.Add(usuarioPassivo);
                db.SaveChanges();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
             }
        }
    }
}