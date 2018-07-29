using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class AprobacionViatico
    {
        public int IdAprobacionViatico { get; set; }
        public int IdEmpleado { get; set; }
        public int? IdSolicitudViatico { get; set; }
        public decimal? ValorJustificado { get; set; }
        public string ValorAdescontar { get; set; }

        public virtual SolicitudViatico IdSolicitudViaticoNavigation { get; set; }
    }
}
