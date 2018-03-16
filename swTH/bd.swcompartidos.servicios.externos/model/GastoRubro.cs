using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class GastoRubro
    {
        public int IdGastoRubro { get; set; }
        public int? IdRubro { get; set; }
        public int IdEmpleadoImpuestoRenta { get; set; }
        public decimal GastoProyectado { get; set; }

        public virtual EmpleadoImpuestoRenta IdEmpleadoImpuestoRentaNavigation { get; set; }
        public virtual Rubro IdRubroNavigation { get; set; }
    }
}
