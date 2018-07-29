using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class ItemViatico
    {
        public ItemViatico()
        {
            DetalleReliquidacionViatico = new HashSet<DetalleReliquidacionViatico>();
            FacturaViatico = new HashSet<FacturaViatico>();
        }

        public int IdItemViatico { get; set; }
        public string Descripcion { get; set; }
        public bool? Reliquidacion { get; set; }

        public virtual ICollection<DetalleReliquidacionViatico> DetalleReliquidacionViatico { get; set; }
        public virtual ICollection<FacturaViatico> FacturaViatico { get; set; }
    }
}
