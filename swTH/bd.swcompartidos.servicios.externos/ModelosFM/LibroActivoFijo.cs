using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class LibroActivoFijo
    {
        public LibroActivoFijo()
        {
            ActivoFijo = new HashSet<ActivoFijo>();
            RecepcionActivoFijo = new HashSet<RecepcionActivoFijo>();
        }

        public int IdLibroActivoFijo { get; set; }
        public int? IdSucursal { get; set; }

        public virtual ICollection<ActivoFijo> ActivoFijo { get; set; }
        public virtual ICollection<RecepcionActivoFijo> RecepcionActivoFijo { get; set; }
        public virtual Sucursal IdSucursalNavigation { get; set; }
    }
}
