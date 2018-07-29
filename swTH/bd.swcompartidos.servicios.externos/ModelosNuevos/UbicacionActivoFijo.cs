using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class UbicacionActivoFijo
    {
        public UbicacionActivoFijo()
        {
            AltaActivoFijoDetalle = new HashSet<AltaActivoFijoDetalle>();
            TransferenciaActivoFijoDetalleIdUbicacionActivoFijoDestinoNavigation = new HashSet<TransferenciaActivoFijoDetalle>();
            TransferenciaActivoFijoDetalleIdUbicacionActivoFijoOrigenNavigation = new HashSet<TransferenciaActivoFijoDetalle>();
        }

        public int IdUbicacionActivoFijo { get; set; }
        public int? IdEmpleado { get; set; }
        public int? IdBodega { get; set; }
        public int IdRecepcionActivoFijoDetalle { get; set; }
        public int IdLibroActivoFijo { get; set; }
        public DateTime FechaUbicacion { get; set; }

        public virtual ICollection<AltaActivoFijoDetalle> AltaActivoFijoDetalle { get; set; }
        public virtual ICollection<TransferenciaActivoFijoDetalle> TransferenciaActivoFijoDetalleIdUbicacionActivoFijoDestinoNavigation { get; set; }
        public virtual ICollection<TransferenciaActivoFijoDetalle> TransferenciaActivoFijoDetalleIdUbicacionActivoFijoOrigenNavigation { get; set; }
        public virtual Bodega IdBodegaNavigation { get; set; }
        public virtual Empleado IdEmpleadoNavigation { get; set; }
        public virtual LibroActivoFijo IdLibroActivoFijoNavigation { get; set; }
        public virtual RecepcionActivoFijoDetalle IdRecepcionActivoFijoDetalleNavigation { get; set; }
    }
}
