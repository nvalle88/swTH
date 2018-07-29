using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Presupuesto
    {
        public Presupuesto()
        {
            DetallePresupuesto = new HashSet<DetallePresupuesto>();
            ReliquidacionViatico = new HashSet<ReliquidacionViatico>();
        }

        public int IdPresupuesto { get; set; }
        public string NumeroPartidaPresupuestaria { get; set; }
        public double? Valor { get; set; }
        public DateTime? Fecha { get; set; }
        public int? IdSucursal { get; set; }

        public virtual ICollection<DetallePresupuesto> DetallePresupuesto { get; set; }
        public virtual ICollection<ReliquidacionViatico> ReliquidacionViatico { get; set; }
        public virtual Sucursal IdSucursalNavigation { get; set; }
    }
}
