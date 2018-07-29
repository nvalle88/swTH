using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class SolicitudAcumulacionDecimos
    {
        public int IdSolicitudAcumulacionDecimos { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public bool AcumulaDecimoCuarto { get; set; }
        public bool AcumulaDecimoTercero { get; set; }
    }
}
