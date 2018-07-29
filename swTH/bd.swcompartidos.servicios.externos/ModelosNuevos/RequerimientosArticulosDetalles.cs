using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class RequerimientosArticulosDetalles
    {
        public int IdRequerimientosArticulos { get; set; }
        public int IdMaestroArticuloSucursal { get; set; }
        public int CantidadSolicitada { get; set; }
        public int CantidadAprobada { get; set; }

        public virtual MaestroArticuloSucursal IdMaestroArticuloSucursalNavigation { get; set; }
        public virtual RequerimientoArticulos IdRequerimientosArticulosNavigation { get; set; }
    }
}
