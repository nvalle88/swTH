using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class Estado
    {
        public Estado()
        {
            RecepcionActivoFijoDetalle = new HashSet<RecepcionActivoFijoDetalle>();
            SolicitudAnticipo = new HashSet<SolicitudAnticipo>();
            SolicitudModificacionFichaEmpleado = new HashSet<SolicitudModificacionFichaEmpleado>();
            SolicitudProveeduriaDetalle = new HashSet<SolicitudProveeduriaDetalle>();
        }

        public int IdEstado { get; set; }
        public int? IdSolicitudCertificadoPersonal { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<RecepcionActivoFijoDetalle> RecepcionActivoFijoDetalle { get; set; }
        public virtual ICollection<SolicitudAnticipo> SolicitudAnticipo { get; set; }
        public virtual ICollection<SolicitudModificacionFichaEmpleado> SolicitudModificacionFichaEmpleado { get; set; }
        public virtual ICollection<SolicitudProveeduriaDetalle> SolicitudProveeduriaDetalle { get; set; }
    }
}
