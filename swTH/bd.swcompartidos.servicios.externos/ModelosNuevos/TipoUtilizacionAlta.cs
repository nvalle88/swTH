using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class TipoUtilizacionAlta
    {
        public TipoUtilizacionAlta()
        {
            AltaActivoFijoDetalle = new HashSet<AltaActivoFijoDetalle>();
        }

        public int IdTipoUtilizacionAlta { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<AltaActivoFijoDetalle> AltaActivoFijoDetalle { get; set; }
    }
}
