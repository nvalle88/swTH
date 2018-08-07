using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bd.swth.entidades.Negocio
{
   public class PeriodoNomina
    {
        [Key]
        public int IdPeriodo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Descripcion { get; set; }
        public bool Abierto { get; set; }
        public string Estado { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
    }
}
