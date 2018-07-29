using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class PeriodoNomina
    {
        public PeriodoNomina()
        {
            CalculoNomina = new HashSet<CalculoNomina>();
        }

        public int IdPeriodo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Descripcion { get; set; }
        public bool Abierto { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<CalculoNomina> CalculoNomina { get; set; }
    }
}
