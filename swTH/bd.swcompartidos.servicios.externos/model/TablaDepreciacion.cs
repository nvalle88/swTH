using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class TablaDepreciacion
    {
        public TablaDepreciacion()
        {
            ClaseActivoFijo = new HashSet<ClaseActivoFijo>();
        }

        public int IdTablaDepreciacion { get; set; }
        public decimal IndiceDepreciacion { get; set; }

        public virtual ICollection<ClaseActivoFijo> ClaseActivoFijo { get; set; }
    }
}
