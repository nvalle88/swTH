using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class PersonaDiscapacidad
    {
        public PersonaDiscapacidad()
        {
            PersonaSustituto = new HashSet<PersonaSustituto>();
        }

        public int IdPersonaDiscapacidad { get; set; }
        public int? IdTipoDiscapacidad { get; set; }
        public int? IdPersona { get; set; }
        public string NumeroCarnet { get; set; }
        public int Porciento { get; set; }

        public virtual ICollection<PersonaSustituto> PersonaSustituto { get; set; }
        public virtual Persona IdPersonaNavigation { get; set; }
        public virtual TipoDiscapacidad IdTipoDiscapacidadNavigation { get; set; }
    }
}
