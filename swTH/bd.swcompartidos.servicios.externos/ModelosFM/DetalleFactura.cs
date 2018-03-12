using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class DetalleFactura
    {
        public int IdDetalleFactura { get; set; }
        public int IdFactura { get; set; }
        public int IdArticulo { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        public virtual Articulo IdArticuloNavigation { get; set; }
        public virtual Factura IdFacturaNavigation { get; set; }
    }
}
