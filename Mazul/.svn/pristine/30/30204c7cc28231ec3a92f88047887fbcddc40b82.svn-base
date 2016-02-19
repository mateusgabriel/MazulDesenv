using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgendaApp.Entidades;
using System.Data.Entity;

namespace AgendaApp
{
    public class AgendaAppContext : DbContext
    {
        public AgendaAppContext() : base("name=AgendaAppContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UsuarioPassivo> UsuariosPassivos { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<UsuarioAtivo> UsuariosAtivos { get; set; }
    }
}