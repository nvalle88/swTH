using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class SolicitudAnticipo
    {
        public int IdSolicitudAnticipo { get; set; }
        public int IdEmpleado { get; set; }
        public int? IdEstado { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public decimal CantidadSolicitada { get; set; }
        public decimal CantidadCancelada { get; set; }

        public virtual Estado IdEstadoNavigation { get; set; }
    }
}
