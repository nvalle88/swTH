using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class MotivoRecepcion
    {
        public MotivoRecepcion()
        {
            RecepcionActivoFijo = new HashSet<RecepcionActivoFijo>();
        }

        public int IdMotivoRecepcion { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<RecepcionActivoFijo> RecepcionActivoFijo { get; set; }
    }
}
