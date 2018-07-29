using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class InventarioActivoFijoDetalle
    {
        public int IdRecepcionActivoFijoDetalle { get; set; }
        public int IdInventarioActivoFijo { get; set; }
        public bool Constatado { get; set; }

        public virtual InventarioActivoFijo IdInventarioActivoFijoNavigation { get; set; }
        public virtual RecepcionActivoFijoDetalle IdRecepcionActivoFijoDetalleNavigation { get; set; }
    }
}
