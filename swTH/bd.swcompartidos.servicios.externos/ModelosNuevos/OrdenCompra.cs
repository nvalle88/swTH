using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class OrdenCompra
    {
        public OrdenCompra()
        {
            OrdenCompraDetalles = new HashSet<OrdenCompraDetalles>();
        }

        public int IdOrdenCompra { get; set; }
        public int IdMotivoRecepcionArticulos { get; set; }
        public DateTime Fecha { get; set; }
        public int IdEstado { get; set; }
        public int IdFacturaActivoFijo { get; set; }
        public int IdEmpleadoResponsable { get; set; }
        public int IdBodega { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdEmpleadoDevolucion { get; set; }
        public string Codigo { get; set; }

        public virtual ICollection<OrdenCompraDetalles> OrdenCompraDetalles { get; set; }
        public virtual Bodega IdBodegaNavigation { get; set; }
        public virtual Empleado IdEmpleadoDevolucionNavigation { get; set; }
        public virtual Empleado IdEmpleadoResponsableNavigation { get; set; }
        public virtual Estado IdEstadoNavigation { get; set; }
        public virtual FacturaActivoFijo IdFacturaActivoFijoNavigation { get; set; }
        public virtual MotivoRecepcionArticulos IdMotivoRecepcionArticulosNavigation { get; set; }
        public virtual Proveedor IdProveedorNavigation { get; set; }
    }
}
