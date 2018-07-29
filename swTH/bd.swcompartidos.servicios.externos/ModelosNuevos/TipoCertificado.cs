using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class TipoCertificado
    {
        public TipoCertificado()
        {
            SolicitudCertificadoPersonal = new HashSet<SolicitudCertificadoPersonal>();
        }

        public int IdTipoCertificado { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<SolicitudCertificadoPersonal> SolicitudCertificadoPersonal { get; set; }
    }
}
