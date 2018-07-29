using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class RevalorizacionActivoFijo
    {
        public int IdRevalorizacionActivoFijo { get; set; }
        public decimal ValorCompra { get; set; }
        public DateTime FechaRevalorizacion { get; set; }
        public int IdRecepcionActivoFijoDetalle { get; set; }

        public virtual RecepcionActivoFijoDetalle IdRecepcionActivoFijoDetalleNavigation { get; set; }
    }
}
