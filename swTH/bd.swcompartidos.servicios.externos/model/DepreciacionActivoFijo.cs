using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class DepreciacionActivoFijo
    {
        public int IdDepreciacionActivoFijo { get; set; }
        public int IdActivoFijo { get; set; }
        public DateTime FechaDepreciacion { get; set; }
        public decimal DepreciacionAcumulada { get; set; }
        public decimal ValorResidual { get; set; }

        public virtual ActivoFijo IdActivoFijoNavigation { get; set; }
    }
}
