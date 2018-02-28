using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class SolicitudLiquidacionHaberes
    {
        public int IdSolicitudLiquidacionHaberes { get; set; }
        public int? IdEmpleado { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public decimal TotalVacaciones { get; set; }
        public decimal TotalDecimoTercero { get; set; }
        public decimal TotalDecimoCuarto { get; set; }
        public decimal? TotalFondoReserva { get; set; }
        public decimal? TotalDesahucio { get; set; }
        public decimal? TotalDespidoIntempestivo { get; set; }
    }
}
