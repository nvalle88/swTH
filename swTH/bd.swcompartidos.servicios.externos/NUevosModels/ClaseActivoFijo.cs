using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class ClaseActivoFijo
    {
        public ClaseActivoFijo()
        {
            SubClaseActivoFijo = new HashSet<SubClaseActivoFijo>();
        }

        public int IdClaseActivoFijo { get; set; }
        public int IdTipoActivoFijo { get; set; }
        public int IdTablaDepreciacion { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<SubClaseActivoFijo> SubClaseActivoFijo { get; set; }
        public virtual TablaDepreciacion IdTablaDepreciacionNavigation { get; set; }
        public virtual TipoActivoFijo IdTipoActivoFijoNavigation { get; set; }
    }
}
