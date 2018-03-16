using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class TipoActivoFijo
    {
        public TipoActivoFijo()
        {
            ClaseActivoFijo = new HashSet<ClaseActivoFijo>();
        }

        public int IdTipoActivoFijo { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<ClaseActivoFijo> ClaseActivoFijo { get; set; }
    }
}
