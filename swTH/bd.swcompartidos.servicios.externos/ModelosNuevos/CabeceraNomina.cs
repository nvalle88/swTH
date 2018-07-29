using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class CabeceraNomina
    {
        public CabeceraNomina()
        {
            DetalleNomina = new HashSet<DetalleNomina>();
        }

        public int IdCabeceraNomina { get; set; }
        public int? IdCalculoNomina { get; set; }
        public int? IdEmpleado { get; set; }
        public string Identificacion { get; set; }

        public virtual ICollection<DetalleNomina> DetalleNomina { get; set; }
        public virtual CalculoNomina IdCalculoNominaNavigation { get; set; }
    }
}
