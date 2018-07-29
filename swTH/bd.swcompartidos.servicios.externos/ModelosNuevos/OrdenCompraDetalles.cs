using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class OrdenCompraDetalles
    {
        public int IdOrdenCompra { get; set; }
        public int IdMaestroArticuloSucursal { get; set; }
        public decimal ValorUnitario { get; set; }
        public int Cantidad { get; set; }

        public virtual MaestroArticuloSucursal IdMaestroArticuloSucursalNavigation { get; set; }
        public virtual OrdenCompra IdOrdenCompraNavigation { get; set; }
    }
}
