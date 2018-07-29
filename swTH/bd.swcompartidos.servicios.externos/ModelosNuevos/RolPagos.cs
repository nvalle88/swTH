using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class RolPagos
    {
        public RolPagos()
        {
            RolPagoDetalle = new HashSet<RolPagoDetalle>();
        }

        public int IdRolPagos { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime Fecha { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal SaldoFinal { get; set; }

        public virtual ICollection<RolPagoDetalle> RolPagoDetalle { get; set; }
        public virtual Empleado IdEmpleadoNavigation { get; set; }
    }
}
