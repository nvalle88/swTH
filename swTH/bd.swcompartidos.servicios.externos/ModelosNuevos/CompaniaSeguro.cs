using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class CompaniaSeguro
    {
        public CompaniaSeguro()
        {
            PolizaSeguroActivoFijo = new HashSet<PolizaSeguroActivoFijo>();
        }

        public int IdCompaniaSeguro { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicioVigencia { get; set; }
        public DateTime FechaFinVigencia { get; set; }

        public virtual ICollection<PolizaSeguroActivoFijo> PolizaSeguroActivoFijo { get; set; }
    }
}
