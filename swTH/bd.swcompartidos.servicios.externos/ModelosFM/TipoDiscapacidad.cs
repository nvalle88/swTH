using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class TipoDiscapacidad
    {
        public TipoDiscapacidad()
        {
            DiscapacidadSustituto = new HashSet<DiscapacidadSustituto>();
            PersonaDiscapacidad = new HashSet<PersonaDiscapacidad>();
        }

        public int IdTipoDiscapacidad { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<DiscapacidadSustituto> DiscapacidadSustituto { get; set; }
        public virtual ICollection<PersonaDiscapacidad> PersonaDiscapacidad { get; set; }
    }
}
