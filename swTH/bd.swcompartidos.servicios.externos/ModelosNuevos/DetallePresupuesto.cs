using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class DetallePresupuesto
    {
        public int IdDetallePresupuesto { get; set; }
        public int? IdPresupuesto { get; set; }
        public int? IdSolicitudViatico { get; set; }
        public double? Valor { get; set; }
        public DateTime? Fecha { get; set; }

        public virtual Presupuesto IdPresupuestoNavigation { get; set; }
        public virtual SolicitudViatico IdSolicitudViaticoNavigation { get; set; }
    }
}
