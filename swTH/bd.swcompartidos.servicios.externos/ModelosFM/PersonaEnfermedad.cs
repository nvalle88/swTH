using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class PersonaEnfermedad
    {
        public int IdPersonaEnfermedad { get; set; }
        public int? IdTipoEnfermedad { get; set; }
        public int? IdPersona { get; set; }
        public string InstitucionEmite { get; set; }

        public virtual Persona IdPersonaNavigation { get; set; }
        public virtual TipoEnfermedad IdTipoEnfermedadNavigation { get; set; }
    }
}
