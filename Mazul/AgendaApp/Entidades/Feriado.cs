using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgendaApp.Entidades
{
    public class Feriado
    {
        public DateTime Data;
        public string Descricao;

        public Feriado(DateTime DataFeriado, string Descricao)
        {
            this.Data = DataFeriado;
            this.Descricao = Descricao;
        }
    }
}