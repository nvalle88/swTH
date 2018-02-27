using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class IngresoEgresoRmu
    {
        public IngresoEgresoRmu()
        {
            EmpleadoIe = new HashSet<EmpleadoIe>();
        }

        public int IdIngresoEgresoRmu { get; set; }
        public int? IdFormulaRmu { get; set; }
        public string Descripcion { get; set; }
        public string CuentaContable { get; set; }

        public virtual ICollection<EmpleadoIe> EmpleadoIe { get; set; }
        public virtual FormulasRmu IdFormulaRmuNavigation { get; set; }
    }
}
