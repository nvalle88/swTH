using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class SalidaArticulos
    {
        public int IdSalidaArticulos { get; set; }
        public int IdMotivoSalidaArticulos { get; set; }
        public string DescripcionMotivo { get; set; }
        public int? IdEmpleadoRealizaBaja { get; set; }
        public int? IdProveedorDevolucion { get; set; }
        public int? IdEmpleadoDespacho { get; set; }
        public int IdRequerimientoArticulos { get; set; }

        public virtual Empleado IdEmpleadoDespachoNavigation { get; set; }
        public virtual Empleado IdEmpleadoRealizaBajaNavigation { get; set; }
        public virtual MotivoSalidaArticulos IdMotivoSalidaArticulosNavigation { get; set; }
        public virtual Proveedor IdProveedorDevolucionNavigation { get; set; }
        public virtual RequerimientoArticulos IdRequerimientoArticulosNavigation { get; set; }
    }
}
