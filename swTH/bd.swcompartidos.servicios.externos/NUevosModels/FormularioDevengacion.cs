using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class FormularioDevengacion
    {
        public FormularioDevengacion()
        {
            EmpleadosFormularioDevengacion = new HashSet<EmpleadosFormularioDevengacion>();
            MaterialApoyo = new HashSet<MaterialApoyo>();
        }

        public int IdFormularioDevengacion { get; set; }
        public string ModoSocial { get; set; }
        public int? IdEvento { get; set; }
        public int IdModosScializacion { get; set; }
        public int IdEmpleado { get; set; }
        public int AnalistaDesarrolloInstitucional { get; set; }
        public int ResponsableArea { get; set; }

        public virtual ICollection<EmpleadosFormularioDevengacion> EmpleadosFormularioDevengacion { get; set; }
        public virtual ICollection<MaterialApoyo> MaterialApoyo { get; set; }
        public virtual ModosScializacion IdModosScializacionNavigation { get; set; }
    }
}
