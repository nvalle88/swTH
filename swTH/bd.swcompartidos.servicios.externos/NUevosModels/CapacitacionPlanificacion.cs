using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class CapacitacionPlanificacion
    {
        public int IdCapacitacionPlanificacion { get; set; }
        public int IdCapacitacionTemario { get; set; }
        public int IdEmpleado { get; set; }
        public int IdCapacitacionModalidad { get; set; }
        public int? NumeroHoras { get; set; }
        public decimal? Presupuesto { get; set; }
        public DateTime? Fecha { get; set; }

        public virtual CapacitacionModalidad IdCapacitacionModalidadNavigation { get; set; }
        public virtual CapacitacionTemario IdCapacitacionTemarioNavigation { get; set; }
    }
}
