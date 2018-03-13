using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class EmpleadoNepotismo
    {
        public int IdEmpleadoNepotismo { get; set; }
        public int? IdEmpleado { get; set; }
        public int? IdEmpleadoFamiliar { get; set; }
    }
}
