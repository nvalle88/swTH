using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
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
        public int IdSolicitudViatico { get; set; }
        public int AprobadoPor { get; set; }
        public string Url { get; set; }

        public virtual ItemViatico IdItemViaticoNavigation { get; set; }
        public virtual SolicitudViatico IdSolicitudViaticoNavigation { get; set; }
    }
}
