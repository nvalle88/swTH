using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class ActivoFijoMotivoBaja
    {
        public ActivoFijoMotivoBaja()
        {
            ActivosFijosBaja = new HashSet<ActivosFijosBaja>();
        }

        public int IdActivoFijoMotivoBaja { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<ActivosFijosBaja> ActivosFijosBaja { get; set; }
    }
}
