using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class FondoFinanciamiento
    {
        public FondoFinanciamiento()
        {
            EmpleadoMovimiento = new HashSet<EmpleadoMovimiento>();
            IndiceOcupacionalModalidadPartida = new HashSet<IndiceOcupacionalModalidadPartida>();
            RecepcionActivoFijo = new HashSet<RecepcionActivoFijo>();
            SolicitudViatico = new HashSet<SolicitudViatico>();
        }

        public int IdFondoFinanciamiento { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<EmpleadoMovimiento> EmpleadoMovimiento { get; set; }
        public virtual ICollection<IndiceOcupacionalModalidadPartida> IndiceOcupacionalModalidadPartida { get; set; }
        public virtual ICollection<RecepcionActivoFijo> RecepcionActivoFijo { get; set; }
        public virtual ICollection<SolicitudViatico> SolicitudViatico { get; set; }
    }
}
