using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class ActivosFijosAlta
    {
        public int IdActivoFijo { get; set; }
        public DateTime? FechaAlta { get; set; }
        public int? IdFactura { get; set; }

        public virtual ActivoFijo IdActivoFijoNavigation { get; set; }
        public virtual Factura IdFacturaNavigation { get; set; }
    }
}
