using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class LibroActivoFijo
    {
        public LibroActivoFijo()
        {
            UbicacionActivoFijo = new HashSet<UbicacionActivoFijo>();
        }

        public int IdLibroActivoFijo { get; set; }
        public int? IdSucursal { get; set; }

        public virtual ICollection<UbicacionActivoFijo> UbicacionActivoFijo { get; set; }
        public virtual Sucursal IdSucursalNavigation { get; set; }
    }
}
