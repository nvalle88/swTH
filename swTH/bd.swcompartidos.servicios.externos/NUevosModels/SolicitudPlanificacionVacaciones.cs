using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class SolicitudPlanificacionVacaciones
    {
        public int IdSolicitudPlanificacionVacaciones { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public int? Estado { get; set; }
        public string Observaciones { get; set; }
    }
}
