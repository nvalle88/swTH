using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class AltaActivoFijoDetalle
    {
        public int IdRecepcionActivoFijoDetalle { get; set; }
        public int IdAltaActivoFijo { get; set; }
        public int IdTipoUtilizacionAlta { get; set; }
        public int IdUbicacionActivoFijo { get; set; }

        public virtual AltaActivoFijo IdAltaActivoFijoNavigation { get; set; }
        public virtual RecepcionActivoFijoDetalle IdRecepcionActivoFijoDetalleNavigation { get; set; }
        public virtual TipoUtilizacionAlta IdTipoUtilizacionAltaNavigation { get; set; }
        public virtual UbicacionActivoFijo IdUbicacionActivoFijoNavigation { get; set; }
    }
}
