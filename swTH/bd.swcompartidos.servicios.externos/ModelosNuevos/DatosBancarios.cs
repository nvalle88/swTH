using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class DatosBancarios
    {
        public int IdDatosBancarios { get; set; }
        public int IdEmpleado { get; set; }
        public int? IdInstitucionFinanciera { get; set; }
        public string NumeroCuenta { get; set; }
        public bool Ahorros { get; set; }

        public virtual Empleado IdEmpleadoNavigation { get; set; }
        public virtual InstitucionFinanciera IdInstitucionFinancieraNavigation { get; set; }
    }
}
