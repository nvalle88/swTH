using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class VacacionesEmpleado
    {
        public int IdVacaciones { get; set; }
        public int PeriodoFiscal { get; set; }
        public int IdEmpleado { get; set; }
        public int VacacionesGozadas { get; set; }
        public int VacacionesNoGozadas { get; set; }

        public virtual Empleado IdEmpleadoNavigation { get; set; }
    }
}
