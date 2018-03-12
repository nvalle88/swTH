using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class EmpleadoSaldoVacaciones
    {
        public int IdEmpleadoSaldoVacaciones { get; set; }
        public int IdEmpleado { get; set; }
        public decimal SaldoDias { get; set; }
        public decimal SaldoMonetario { get; set; }
    }
}
