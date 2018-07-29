using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class ComponenteActivoFijo
    {
        public int IdComponenteActivoFijo { get; set; }
        public int IdRecepcionActivoFijoDetalleOrigen { get; set; }
        public int IdRecepcionActivoFijoDetalleComponente { get; set; }
        public DateTime FechaAdicion { get; set; }

        public virtual RecepcionActivoFijoDetalle IdRecepcionActivoFijoDetalleComponenteNavigation { get; set; }
        public virtual RecepcionActivoFijoDetalle IdRecepcionActivoFijoDetalleOrigenNavigation { get; set; }
    }
}
