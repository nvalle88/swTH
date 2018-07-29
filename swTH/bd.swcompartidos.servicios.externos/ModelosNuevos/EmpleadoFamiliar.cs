using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class EmpleadoFamiliar
    {
        public int IdEmpleadoFamiliar { get; set; }
        public int IdPersona { get; set; }
        public int IdEmpleado { get; set; }
        public int? IdParentesco { get; set; }

        public virtual Parentesco IdParentescoNavigation { get; set; }
        public virtual Persona IdPersonaNavigation { get; set; }
    }
}
