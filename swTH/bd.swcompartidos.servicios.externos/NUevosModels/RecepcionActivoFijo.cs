using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class RecepcionActivoFijo
    {
        public int IdRecepcionActivoFijo { get; set; }
        public int IdEmpleado { get; set; }
        public int IdProveedor { get; set; }
        public int IdSubClaseActivoFijo { get; set; }
        public int? IdMotivoRecepcion { get; set; }
        public int? IdLibroActivoFijo { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public decimal Cantidad { get; set; }
        public bool ValidacionTecnica { get; set; }
        public string Fondo { get; set; }
        public string OrdenCompra { get; set; }

        public virtual Empleado IdEmpleadoNavigation { get; set; }
        public virtual LibroActivoFijo IdLibroActivoFijoNavigation { get; set; }
        public virtual MotivoRecepcion IdMotivoRecepcionNavigation { get; set; }
        public virtual Proveedor IdProveedorNavigation { get; set; }
        public virtual SubClaseActivoFijo IdSubClaseActivoFijoNavigation { get; set; }
    }
}
