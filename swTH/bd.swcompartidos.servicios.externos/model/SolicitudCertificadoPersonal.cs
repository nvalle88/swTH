using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class SolicitudCertificadoPersonal
    {
        public int IdSolicitudCertificadoPersonal { get; set; }
        public bool IdEstado { get; set; }
        public int IdTipoCertificado { get; set; }
        public int IdEmpleadoSolicitante { get; set; }
        public int IdEmpleadoDirigidoA { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Observaciones { get; set; }

        public virtual TipoCertificado IdTipoCertificadoNavigation { get; set; }
    }
}
