using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class MovilizacionActivoFijo
    {
        public MovilizacionActivoFijo()
        {
            MovilizacionActivoFijoDetalle = new HashSet<MovilizacionActivoFijoDetalle>();
        }

        public int IdMovilizacionActivoFijo { get; set; }
        public int IdEmpleadoSolicita { get; set; }
        public int IdEmpleadoResponsable { get; set; }
        public int IdEmpleadoAutorizado { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaRetorno { get; set; }
        public int IdMotivoTraslado { get; set; }

        public virtual ICollection<MovilizacionActivoFijoDetalle> MovilizacionActivoFijoDetalle { get; set; }
        public virtual Empleado IdEmpleadoAutorizadoNavigation { get; set; }
        public virtual Empleado IdEmpleadoResponsableNavigation { get; set; }
        public virtual Empleado IdEmpleadoSolicitaNavigation { get; set; }
        public virtual MotivoTraslado IdMotivoTrasladoNavigation { get; set; }
    }
}
