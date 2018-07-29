using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Estado
    {
        public Estado()
        {
            InventarioActivoFijo = new HashSet<InventarioActivoFijo>();
            OrdenCompra = new HashSet<OrdenCompra>();
            RecepcionActivoFijoDetalle = new HashSet<RecepcionActivoFijoDetalle>();
            RequerimientoArticulos = new HashSet<RequerimientoArticulos>();
            SolicitudAnticipo = new HashSet<SolicitudAnticipo>();
            SolicitudModificacionFichaEmpleado = new HashSet<SolicitudModificacionFichaEmpleado>();
            TransferenciaActivoFijo = new HashSet<TransferenciaActivoFijo>();
        }

        public int IdEstado { get; set; }
        public int? IdSolicitudCertificadoPersonal { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<InventarioActivoFijo> InventarioActivoFijo { get; set; }
        public virtual ICollection<OrdenCompra> OrdenCompra { get; set; }
        public virtual ICollection<RecepcionActivoFijoDetalle> RecepcionActivoFijoDetalle { get; set; }
        public virtual ICollection<RequerimientoArticulos> RequerimientoArticulos { get; set; }
        public virtual ICollection<SolicitudAnticipo> SolicitudAnticipo { get; set; }
        public virtual ICollection<SolicitudModificacionFichaEmpleado> SolicitudModificacionFichaEmpleado { get; set; }
        public virtual ICollection<TransferenciaActivoFijo> TransferenciaActivoFijo { get; set; }
    }
}
