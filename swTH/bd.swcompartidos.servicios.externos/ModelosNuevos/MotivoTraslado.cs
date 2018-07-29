using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class MotivoTraslado
    {
        public MotivoTraslado()
        {
            MovilizacionActivoFijo = new HashSet<MovilizacionActivoFijo>();
        }

        public int IdMotivoTraslado { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<MovilizacionActivoFijo> MovilizacionActivoFijo { get; set; }
    }
}
