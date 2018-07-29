using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class ReliquidacionViatico
    {
        public ReliquidacionViatico()
        {
            DetalleReliquidacionViatico = new HashSet<DetalleReliquidacionViatico>();
        }

        public int IdReliquidacionViatico { get; set; }
        public int IdSolicitudViatico { get; set; }
        public string Descripcion { get; set; }
        public decimal? ValorEstimado { get; set; }
        public bool Estado { get; set; }
        public int? IdPresupuesto { get; set; }

        public virtual ICollection<DetalleReliquidacionViatico> DetalleReliquidacionViatico { get; set; }
        public virtual Presupuesto IdPresupuestoNavigation { get; set; }
        public virtual SolicitudViatico IdSolicitudViaticoNavigation { get; set; }
    }
}
