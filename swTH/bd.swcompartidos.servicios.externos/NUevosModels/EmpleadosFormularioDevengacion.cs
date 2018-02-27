using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class EmpleadosFormularioDevengacion
    {
        public int IdEmpleadosFormularioDevengacion { get; set; }
        public int? IdFormularioDevengacion { get; set; }
        public int IdEmpleado { get; set; }

        public virtual FormularioDevengacion IdFormularioDevengacionNavigation { get; set; }
    }
}
