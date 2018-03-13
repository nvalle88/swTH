using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class FormularioCapacitacion
    {
        public FormularioCapacitacion()
        {
            EmpleadoFormularioCapacitacion = new HashSet<EmpleadoFormularioCapacitacion>();
        }

        public int IdFormularioCapacitacion { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<EmpleadoFormularioCapacitacion> EmpleadoFormularioCapacitacion { get; set; }
    }
}
