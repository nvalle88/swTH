using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class MaestroArticuloSucursal
    {
        public MaestroArticuloSucursal()
        {
            OrdenCompraDetalles = new HashSet<OrdenCompraDetalles>();
            RequerimientosArticulosDetalles = new HashSet<RequerimientosArticulosDetalles>();
        }

        public int IdMaestroArticuloSucursal { get; set; }
        public int IdSucursal { get; set; }
        public int IdArticulo { get; set; }
        public int Minimo { get; set; }
        public int Maximo { get; set; }
        public string CodigoArticulo { get; set; }
        public bool Habilitado { get; set; }
        public DateTime? FechaSinExistencia { get; set; }

        public virtual ICollection<OrdenCompraDetalles> OrdenCompraDetalles { get; set; }
        public virtual ICollection<RequerimientosArticulosDetalles> RequerimientosArticulosDetalles { get; set; }
        public virtual Articulo IdArticuloNavigation { get; set; }
        public virtual Sucursal IdSucursalNavigation { get; set; }
    }
}
