using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class EmpleadoActivoFijo
    {
        public int IdEmpleadoActivoFijo { get; set; }
        public int IdEmpleado { get; set; }
        public int IdActivoFijo { get; set; }
        public DateTime FechaAsignacion { get; set; }

        public virtual ActivoFijo IdActivoFijoNavigation { get; set; }
    }
}
