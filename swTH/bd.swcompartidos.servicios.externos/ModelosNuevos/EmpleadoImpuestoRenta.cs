using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class EmpleadoImpuestoRenta
    {
        public EmpleadoImpuestoRenta()
        {
            GastoRubro = new HashSet<GastoRubro>();
        }

        public int IdEmpleadoImpuestoRenta { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime FechaDesde { get; set; }
        public decimal? IngresoTotal { get; set; }
        public decimal OtrosIngresos { get; set; }

        public virtual ICollection<GastoRubro> GastoRubro { get; set; }
    }
}
