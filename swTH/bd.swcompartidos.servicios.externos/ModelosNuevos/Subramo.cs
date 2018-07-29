using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Subramo
    {
        public Subramo()
        {
            PolizaSeguroActivoFijo = new HashSet<PolizaSeguroActivoFijo>();
        }

        public int IdSubramo { get; set; }
        public int IdRamo { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<PolizaSeguroActivoFijo> PolizaSeguroActivoFijo { get; set; }
        public virtual Ramo IdRamoNavigation { get; set; }
    }
}
