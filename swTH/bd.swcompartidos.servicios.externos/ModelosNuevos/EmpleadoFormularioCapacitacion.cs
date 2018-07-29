using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class EmpleadoFormularioCapacitacion
    {
        public int IdEmpleadoFormularioCapacitacion { get; set; }
        public int? IdEvento { get; set; }
        public int IdFormularioCapacitacion { get; set; }
        public int IdServidor { get; set; }
        public int ResponsableArea { get; set; }
        public int AporbadoPor { get; set; }
        public int RevisadoPor { get; set; }

        public virtual FormularioCapacitacion IdFormularioCapacitacionNavigation { get; set; }
    }
}
