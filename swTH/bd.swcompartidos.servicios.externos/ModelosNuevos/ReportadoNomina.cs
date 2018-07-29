using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class ReportadoNomina
    {
        public int IdReportadoNomina { get; set; }
        public int IdCalculoNomina { get; set; }
        public string CodigoConcepto { get; set; }
        public string IdentificacionEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public double? Cantidad { get; set; }
        public double? Importe { get; set; }

        public virtual CalculoNomina IdCalculoNominaNavigation { get; set; }
    }
}
