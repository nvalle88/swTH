using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class PlanificacionHe
    {
        public int IdPlanificacionHe { get; set; }
        public int IdEmpleado { get; set; }
        public int IdEmpleadoAprueba { get; set; }
        public DateTime Fecha { get; set; }
        public decimal NoHoras { get; set; }
        public bool? Estado { get; set; }
    }
}
