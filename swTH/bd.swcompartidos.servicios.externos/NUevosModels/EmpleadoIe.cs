using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class EmpleadoIe
    {
        public EmpleadoIe()
        {
            Rmu = new HashSet<Rmu>();
        }

        public int IdEmpeladoIe { get; set; }
        public int IdIngresoEgresoRmu { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Valor { get; set; }
        public string Tipo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool? Fijo { get; set; }

        public virtual ICollection<Rmu> Rmu { get; set; }
        public virtual IngresoEgresoRmu IdIngresoEgresoRmuNavigation { get; set; }
    }
}
