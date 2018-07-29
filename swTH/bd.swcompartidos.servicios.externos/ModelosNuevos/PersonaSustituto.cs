using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class PersonaSustituto
    {
        public PersonaSustituto()
        {
            DiscapacidadSustituto = new HashSet<DiscapacidadSustituto>();
            EnfermedadSustituto = new HashSet<EnfermedadSustituto>();
        }

        public int IdPersonaSustituto { get; set; }
        public int IdParentesco { get; set; }
        public int IdPersona { get; set; }
        public int? IdEmpleado { get; set; }

        public virtual ICollection<DiscapacidadSustituto> DiscapacidadSustituto { get; set; }
        public virtual ICollection<EnfermedadSustituto> EnfermedadSustituto { get; set; }
        public virtual Empleado IdEmpleadoNavigation { get; set; }
        public virtual Parentesco IdParentescoNavigation { get; set; }
        public virtual Persona IdPersonaNavigation { get; set; }
    }
}
