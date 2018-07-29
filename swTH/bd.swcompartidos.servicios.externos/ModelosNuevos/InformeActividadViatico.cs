using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class InformeActividadViatico
    {
        public int IdInformeActividad { get; set; }
        public int IdSolicitudViatico { get; set; }
        public string Descripcion { get; set; }
        public string Observacion { get; set; }

        public virtual SolicitudViatico IdSolicitudViaticoNavigation { get; set; }
    }
}
