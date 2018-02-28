using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class MantenimientoActivoFijo
    {
        public int IdMantenimientoActivoFijo { get; set; }
        public int IdEmpleado { get; set; }
        public int IdActivoFijo { get; set; }
        public DateTime FechaMantenimiento { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public decimal Valor { get; set; }
        public string Observaciones { get; set; }

        public virtual ActivoFijo IdActivoFijoNavigation { get; set; }
    }
}
