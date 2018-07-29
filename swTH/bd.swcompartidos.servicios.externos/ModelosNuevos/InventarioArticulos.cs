using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class InventarioArticulos
    {
        public int IdInventarioArticulos { get; set; }
        public int IdArticulo { get; set; }
        public int IdBodega { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }

        public virtual Articulo IdArticuloNavigation { get; set; }
        public virtual Bodega IdBodegaNavigation { get; set; }
    }
}
