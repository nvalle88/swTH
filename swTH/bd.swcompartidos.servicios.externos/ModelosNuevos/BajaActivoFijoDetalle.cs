using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class BajaActivoFijoDetalle
    {
        public int IdRecepcionActivoFijoDetalle { get; set; }
        public int IdBajaActivoFijo { get; set; }

        public virtual BajaActivoFijo IdBajaActivoFijoNavigation { get; set; }
        public virtual RecepcionActivoFijoDetalle IdRecepcionActivoFijoDetalleNavigation { get; set; }
    }
}
