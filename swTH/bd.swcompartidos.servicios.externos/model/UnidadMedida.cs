using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class UnidadMedida
    {
        public UnidadMedida()
        {
            ActivoFijo = new HashSet<ActivoFijo>();
            Articulo = new HashSet<Articulo>();
        }

        public int IdUnidadMedida { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<ActivoFijo> ActivoFijo { get; set; }
        public virtual ICollection<Articulo> Articulo { get; set; }
    }
}
