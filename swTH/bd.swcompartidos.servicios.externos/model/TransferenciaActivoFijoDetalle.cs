using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class TransferenciaActivoFijoDetalle
    {
        public int IdTransferenciaActivoFijoDetalle { get; set; }
        public int IdTransferenciaActivoFijo { get; set; }
        public int? IdActivoFijo { get; set; }

        public virtual ActivoFijo IdActivoFijoNavigation { get; set; }
        public virtual TransferenciaActivoFijo IdTransferenciaActivoFijoNavigation { get; set; }
    }
}
