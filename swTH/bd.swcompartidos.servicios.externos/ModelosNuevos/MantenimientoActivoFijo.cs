using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class MantenimientoActivoFijo
    {
        public int IdMantenimientoActivoFijo { get; set; }
        public int IdEmpleado { get; set; }
        public int IdRecepcionActivoFijoDetalle { get; set; }
        public DateTime FechaMantenimiento { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public decimal Valor { get; set; }
        public string Observaciones { get; set; }

        public virtual Empleado IdEmpleadoNavigation { get; set; }
        public virtual RecepcionActivoFijoDetalle IdRecepcionActivoFijoDetalleNavigation { get; set; }
    }
}
