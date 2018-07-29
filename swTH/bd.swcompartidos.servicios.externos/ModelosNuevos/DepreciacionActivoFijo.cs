using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class DepreciacionActivoFijo
    {
        public int IdDepreciacionActivoFijo { get; set; }
        public DateTime FechaDepreciacion { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal DepreciacionAcumulada { get; set; }
        public decimal ValorResidual { get; set; }
        public int IdRecepcionActivoFijoDetalle { get; set; }

        public virtual RecepcionActivoFijoDetalle IdRecepcionActivoFijoDetalleNavigation { get; set; }
    }
}
