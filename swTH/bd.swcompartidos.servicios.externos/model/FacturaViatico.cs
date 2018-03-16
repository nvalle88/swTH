using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class FacturaViatico
    {
        public int IdFacturaViatico { get; set; }
        public string NumeroFactura { get; set; }
        public DateTime FechaFactura { get; set; }
        public decimal ValorTotalFactura { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public DateTime? ValorTotalAprobacion { get; set; }
        public string Observaciones { get; set; }
        public int IdItemViatico { get; set; }
        public int IdItinerarioViatico { get; set; }
        public int AprobadoPor { get; set; }

        public virtual ItemViatico IdItemViaticoNavigation { get; set; }
        public virtual ItinerarioViatico IdItinerarioViaticoNavigation { get; set; }
    }
}
