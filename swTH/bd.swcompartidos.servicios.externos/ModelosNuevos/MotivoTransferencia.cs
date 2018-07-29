using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class MotivoTransferencia
    {
        public MotivoTransferencia()
        {
            TransferenciaActivoFijo = new HashSet<TransferenciaActivoFijo>();
        }

        public int IdMotivoTransferencia { get; set; }
        public string MotivoTransferencia1 { get; set; }

        public virtual ICollection<TransferenciaActivoFijo> TransferenciaActivoFijo { get; set; }
    }
}
