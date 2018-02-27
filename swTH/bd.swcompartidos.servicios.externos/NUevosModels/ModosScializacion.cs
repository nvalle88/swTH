using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class ModosScializacion
    {
        public ModosScializacion()
        {
            FormularioDevengacion = new HashSet<FormularioDevengacion>();
        }

        public int IdModosScializacion { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<FormularioDevengacion> FormularioDevengacion { get; set; }
    }
}
