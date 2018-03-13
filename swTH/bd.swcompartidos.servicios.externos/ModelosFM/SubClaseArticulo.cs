using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class SubClaseArticulo
    {
        public SubClaseArticulo()
        {
            Articulo = new HashSet<Articulo>();
        }

        public int IdSubClaseArticulo { get; set; }
        public int IdClaseArticulo { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Articulo> Articulo { get; set; }
        public virtual ClaseArticulo IdClaseArticuloNavigation { get; set; }
    }
}
