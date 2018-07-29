using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Bodega
    {
        public Bodega()
        {
            Dependencia = new HashSet<Dependencia>();
            InventarioArticulos = new HashSet<InventarioArticulos>();
            OrdenCompra = new HashSet<OrdenCompra>();
            UbicacionActivoFijo = new HashSet<UbicacionActivoFijo>();
        }

        public int IdBodega { get; set; }
        public int IdSucursal { get; set; }
        public string Nombre { get; set; }
        public int IdEmpleadoResponsable { get; set; }

        public virtual ICollection<Dependencia> Dependencia { get; set; }
        public virtual ICollection<InventarioArticulos> InventarioArticulos { get; set; }
        public virtual ICollection<OrdenCompra> OrdenCompra { get; set; }
        public virtual ICollection<UbicacionActivoFijo> UbicacionActivoFijo { get; set; }
        public virtual Empleado IdEmpleadoResponsableNavigation { get; set; }
        public virtual Sucursal IdSucursalNavigation { get; set; }
    }
}
