using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class DetalleReliquidacionViatico
    {
        public int IdDetalleReliquidacionViatico { get; set; }
        public int IdReliquidacionViatico { get; set; }
        public int? IdTipoTransporte { get; set; }
        public int? IdItemViatico { get; set; }
        public string NombreTransporte { get; set; }
        public int? IdCiudadOrigen { get; set; }
        public int? IdCiudadDestino { get; set; }
        public DateTime? FechaLlegada { get; set; }
        public DateTime? FechaSalida { get; set; }
        public TimeSpan? HoraLlegada { get; set; }
        public TimeSpan? HoraSalida { get; set; }
        public string Descripcion { get; set; }
        public decimal? ValorEstimado { get; set; }

        public virtual Ciudad IdCiudadDestinoNavigation { get; set; }
        public virtual Ciudad IdCiudadOrigenNavigation { get; set; }
        public virtual ItemViatico IdItemViaticoNavigation { get; set; }
        public virtual ReliquidacionViatico IdReliquidacionViaticoNavigation { get; set; }
        public virtual TipoTransporte IdTipoTransporteNavigation { get; set; }
    }
}
