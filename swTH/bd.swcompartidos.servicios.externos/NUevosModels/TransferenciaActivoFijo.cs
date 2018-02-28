using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class TransferenciaActivoFijo
    {
        public TransferenciaActivoFijo()
        {
            TransferenciaActivoFijoDetalle = new HashSet<TransferenciaActivoFijoDetalle>();
        }

        public int IdTransferenciaActivoFijo { get; set; }
        public int IdEmpleadoRegistra { get; set; }
        public int IdEmpleadoResponsableEnvio { get; set; }
        public int IdEmpleadoResponsableRecibo { get; set; }
        public DateTime FechaTransferencia { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string Observaciones { get; set; }
        public int? IdMotivoTransferencia { get; set; }
        public int? IdEmpleadoRecibo { get; set; }

        public virtual ICollection<TransferenciaActivoFijoDetalle> TransferenciaActivoFijoDetalle { get; set; }
    }
}
