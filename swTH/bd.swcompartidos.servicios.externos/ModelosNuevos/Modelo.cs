using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Modelo
    {
        public Modelo()
        {
            ActivoFijo = new HashSet<ActivoFijo>();
            Articulo = new HashSet<Articulo>();
        }

        public int IdModelo { get; set; }
        public int IdMarca { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<ActivoFijo> ActivoFijo { get; set; }
        public virtual ICollection<Articulo> Articulo { get; set; }
        public virtual Marca IdMarcaNavigation { get; set; }
    }
}
