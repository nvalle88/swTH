using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class TransferenciaActivoFijoDetalle
    {
        public int IdRecepcionActivoFijoDetalle { get; set; }
        public int IdTransferenciaActivoFijo { get; set; }
        public int IdUbicacionActivoFijoOrigen { get; set; }
        public int IdUbicacionActivoFijoDestino { get; set; }
        public int? IdCodigoActivoFijo { get; set; }

        public virtual CodigoActivoFijo IdCodigoActivoFijoNavigation { get; set; }
        public virtual RecepcionActivoFijoDetalle IdRecepcionActivoFijoDetalleNavigation { get; set; }
        public virtual TransferenciaActivoFijo IdTransferenciaActivoFijoNavigation { get; set; }
        public virtual UbicacionActivoFijo IdUbicacionActivoFijoDestinoNavigation { get; set; }
        public virtual UbicacionActivoFijo IdUbicacionActivoFijoOrigenNavigation { get; set; }
    }
}
