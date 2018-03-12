using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class Factura
    {
        public Factura()
        {
            ActivosFijosAlta = new HashSet<ActivosFijosAlta>();
            DetalleFactura = new HashSet<DetalleFactura>();
        }

        public int IdFactura { get; set; }
        public int IdProveedor { get; set; }
        public int IdMaestroArticuloSucursal { get; set; }
        public string Numero { get; set; }

        public virtual ICollection<ActivosFijosAlta> ActivosFijosAlta { get; set; }
        public virtual ICollection<DetalleFactura> DetalleFactura { get; set; }
        public virtual MaestroArticuloSucursal IdMaestroArticuloSucursalNavigation { get; set; }
        public virtual Proveedor IdProveedorNavigation { get; set; }
    }
}
