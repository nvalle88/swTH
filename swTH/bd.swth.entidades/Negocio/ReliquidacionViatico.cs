using System;
using System.Collections.Generic;
namespace bd.swth.entidades.Negocio
{
    public partial class ReliquidacionViatico
    {
        public int IdReliquidacionViatico { get; set; }
        public int IdSolicitudViatico { get; set; }
        public string Descripcion { get; set; }
        public decimal? ValorEstimado { get; set; }
        public bool Estado { get; set; }
        public int? IdPresupuesto { get; set; }

        public virtual ICollection<DetalleReliquidacionViatico> DetalleReliquidacionViatico { get; set; }
        public virtual Presupuesto Presupuesto { get; set; }
        public virtual SolicitudViatico SolicitudViatico { get; set; }
    }
}
