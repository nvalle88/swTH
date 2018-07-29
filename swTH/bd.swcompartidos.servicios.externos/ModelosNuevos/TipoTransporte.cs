using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class TipoTransporte
    {
        public TipoTransporte()
        {
            DetalleReliquidacionViatico = new HashSet<DetalleReliquidacionViatico>();
            InformeViatico = new HashSet<InformeViatico>();
            ItinerarioViatico = new HashSet<ItinerarioViatico>();
        }

        public int IdTipoTransporte { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<DetalleReliquidacionViatico> DetalleReliquidacionViatico { get; set; }
        public virtual ICollection<InformeViatico> InformeViatico { get; set; }
        public virtual ICollection<ItinerarioViatico> ItinerarioViatico { get; set; }
    }
}
