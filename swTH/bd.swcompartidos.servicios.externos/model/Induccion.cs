using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class Induccion
    {
        public int IdInduccion { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime Fecha { get; set; }
    }
}
