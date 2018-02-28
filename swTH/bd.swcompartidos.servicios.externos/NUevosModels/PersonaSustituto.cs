using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
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
        public int IdPersonaDiscapacidad { get; set; }

        public virtual ICollection<DiscapacidadSustituto> DiscapacidadSustituto { get; set; }
        public virtual ICollection<EnfermedadSustituto> EnfermedadSustituto { get; set; }
        public virtual Parentesco IdParentescoNavigation { get; set; }
        public virtual Persona IdPersonaNavigation { get; set; }
        public virtual Persona IdPersonaDiscapacidadNavigation { get; set; }
    }
}
