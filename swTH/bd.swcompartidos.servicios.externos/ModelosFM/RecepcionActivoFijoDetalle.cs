using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class RecepcionActivoFijoDetalle
    {
        public int IdRecepcionActivoFijoDetalle { get; set; }
        public int IdRecepcionActivoFijo { get; set; }
        public int IdActivoFijo { get; set; }
        public int IdEstado { get; set; }
        public string NumeroPoliza { get; set; }

        public virtual Estado IdEstadoNavigation { get; set; }
    }
}
