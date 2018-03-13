using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class RecepcionArticulos
    {
        public int IdRecepcionArticulos { get; set; }
        public int IdEmpleado { get; set; }
        public int IdArticulo { get; set; }
        public int IdMaestroArticuloSucursal { get; set; }
        public int IdProveedor { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }

        public virtual Articulo IdArticuloNavigation { get; set; }
        public virtual Empleado IdEmpleadoNavigation { get; set; }
        public virtual MaestroArticuloSucursal IdMaestroArticuloSucursalNavigation { get; set; }
        public virtual Proveedor IdProveedorNavigation { get; set; }
    }
}
