using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class SolicitudProveeduriaDetalle
    {
        public int IdSolicitudProveeduriaDetalle { get; set; }
        public int IdSolicitudProveduria { get; set; }
        public int IdEstado { get; set; }
        public int IdMaestroArticuloSucursal { get; set; }
        public int IdArticulo { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaAprobada { get; set; }
        public int CantidadSolicitada { get; set; }
        public int CantidadAprobada { get; set; }

        public virtual Estado IdEstadoNavigation { get; set; }
    }
}
