using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class ClaseArticulo
    {
        public ClaseArticulo()
        {
            SubClaseArticulo = new HashSet<SubClaseArticulo>();
        }

        public int IdClaseArticulo { get; set; }
        public int IdTipoArticulo { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<SubClaseArticulo> SubClaseArticulo { get; set; }
        public virtual TipoArticulo IdTipoArticuloNavigation { get; set; }
    }
}
