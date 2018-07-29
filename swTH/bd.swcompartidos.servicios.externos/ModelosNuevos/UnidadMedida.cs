using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class UnidadMedida
    {
        public UnidadMedida()
        {
            Articulo = new HashSet<Articulo>();
        }

        public int IdUnidadMedida { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Articulo> Articulo { get; set; }
    }
}
