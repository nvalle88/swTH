using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class TransferenciaActivoFijo
    {
        public TransferenciaActivoFijo()
        {
            TransferenciaActivoFijoDetalle = new HashSet<TransferenciaActivoFijoDetalle>();
        }

        public int IdTransferenciaActivoFijo { get; set; }
        public int IdEstado { get; set; }
        public int? IdEmpleadoResponsableEnvio { get; set; }
        public int? IdEmpleadoResponsableRecibo { get; set; }
        public DateTime FechaTransferencia { get; set; }
        public string Observaciones { get; set; }
        public int IdMotivoTransferencia { get; set; }

        public virtual ICollection<TransferenciaActivoFijoDetalle> TransferenciaActivoFijoDetalle { get; set; }
        public virtual Empleado IdEmpleadoResponsableEnvioNavigation { get; set; }
        public virtual Empleado IdEmpleadoResponsableReciboNavigation { get; set; }
        public virtual Estado IdEstadoNavigation { get; set; }
        public virtual MotivoTransferencia IdMotivoTransferenciaNavigation { get; set; }
    }
}
