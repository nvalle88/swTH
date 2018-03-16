using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class Rmu
    {
        public int IdRmu { get; set; }
        public int IdEmpleado { get; set; }
        public int IdEmpeladoIe { get; set; }
        public int IdTipoRmu { get; set; }
        public DateTime Fecha { get; set; }
        public int Quincena { get; set; }
        public decimal? Valor { get; set; }

        public virtual EmpleadoIe IdEmpeladoIeNavigation { get; set; }
        public virtual Empleado IdEmpleadoNavigation { get; set; }
        public virtual TipoRmu IdTipoRmuNavigation { get; set; }
    }
}
