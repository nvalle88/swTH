using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class TipoEnfermedad
    {
        public TipoEnfermedad()
        {
            EnfermedadSustituto = new HashSet<EnfermedadSustituto>();
            PersonaEnfermedad = new HashSet<PersonaEnfermedad>();
        }

        public int IdTipoEnfermedad { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<EnfermedadSustituto> EnfermedadSustituto { get; set; }
        public virtual ICollection<PersonaEnfermedad> PersonaEnfermedad { get; set; }
    }
}
