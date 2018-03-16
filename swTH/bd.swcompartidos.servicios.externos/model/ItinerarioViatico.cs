using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class ItinerarioViatico
    {
        public ItinerarioViatico()
        {
            FacturaViatico = new HashSet<FacturaViatico>();
            InformeViatico = new HashSet<InformeViatico>();
        }

        public int IdItinerarioViatico { get; set; }
        public int IdSolicitudViatico { get; set; }
        public int IdTipoTransporte { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public decimal Valor { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public TimeSpan HoraLlegada { get; set; }

        public virtual ICollection<FacturaViatico> FacturaViatico { get; set; }
        public virtual ICollection<InformeViatico> InformeViatico { get; set; }
        public virtual SolicitudViatico IdSolicitudViaticoNavigation { get; set; }
        public virtual TipoTransporte IdTipoTransporteNavigation { get; set; }
    }
}
