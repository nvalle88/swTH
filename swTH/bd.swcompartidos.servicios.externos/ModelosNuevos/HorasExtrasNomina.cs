using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class HorasExtrasNomina
    {
        public int IdHorasExtrasNomina { get; set; }
        public int? CantidadHoras { get; set; }
        public string IdentificacionEmpleado { get; set; }
        public bool? EsExtraordinaria { get; set; }
        public int? IdCalculoNomina { get; set; }
        public int? IdEmpleado { get; set; }

        public virtual CalculoNomina IdCalculoNominaNavigation { get; set; }
        public virtual Empleado IdEmpleadoNavigation { get; set; }
    }
}
