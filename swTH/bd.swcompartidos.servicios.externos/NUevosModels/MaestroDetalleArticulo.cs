using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class MaestroDetalleArticulo
    {
        public int IdMaestroDetalleArticulo { get; set; }
        public int IdMaestroArticuloSucursal { get; set; }
        public int IdArticulo { get; set; }
        public int Cantidad { get; set; }

        public virtual Articulo IdArticuloNavigation { get; set; }
        public virtual MaestroArticuloSucursal IdMaestroArticuloSucursalNavigation { get; set; }
    }
}
