using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class EmpleadoNepotismo
    {
        public int IdEmpleadoNepotismo { get; set; }
        public int? IdEmpleado { get; set; }
        public int? IdEmpleadoFamiliar { get; set; }
    }
}
