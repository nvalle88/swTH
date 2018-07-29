using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class TipoArticulo
    {
        public TipoArticulo()
        {
            ClaseArticulo = new HashSet<ClaseArticulo>();
        }

        public int IdTipoArticulo { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<ClaseArticulo> ClaseArticulo { get; set; }
    }
}
