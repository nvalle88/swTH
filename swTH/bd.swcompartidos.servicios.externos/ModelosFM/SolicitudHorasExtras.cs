using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class SolicitudHorasExtras
    {
        public long IdSolicitudHorasExtras { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime Fecha { get; set; }
        public int CantidadHoras { get; set; }
        public string Observaciones { get; set; }
        public int Estado { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaAprobado { get; set; }
        public bool EsExtraordinaria { get; set; }
    }
}
