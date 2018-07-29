using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class LineaServicio
    {
        public LineaServicio()
        {
            Proveedor = new HashSet<Proveedor>();
        }

        public int IdLineaServicio { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Proveedor> Proveedor { get; set; }
    }
}
