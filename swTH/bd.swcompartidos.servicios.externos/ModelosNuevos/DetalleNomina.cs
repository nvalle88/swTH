using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class DetalleNomina
    {
        public int IdDetalleNomina { get; set; }
        public int? IdCabeceraNomina { get; set; }
        public string CodigoConceptoNomina { get; set; }
        public decimal? Valor { get; set; }
        public int? Signo { get; set; }

        public virtual CabeceraNomina IdCabeceraNominaNavigation { get; set; }
    }
}
