using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class CategoriaActivoFijo
    {
        public CategoriaActivoFijo()
        {
            ClaseActivoFijo = new HashSet<ClaseActivoFijo>();
        }

        public int IdCategoriaActivoFijo { get; set; }
        public string Nombre { get; set; }
        public decimal PorCientoDepreciacionAnual { get; set; }
        public int AnosVidaUtil { get; set; }

        public virtual ICollection<ClaseActivoFijo> ClaseActivoFijo { get; set; }
    }
}
