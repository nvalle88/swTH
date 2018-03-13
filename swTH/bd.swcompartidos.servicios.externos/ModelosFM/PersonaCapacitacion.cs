using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class PersonaCapacitacion
    {
        public int IdPersonaCapacitacion { get; set; }
        public string Observaciones { get; set; }
        public DateTime? Fecha { get; set; }
        public int IdCapacitacion { get; set; }
        public int IdPersona { get; set; }

        public virtual Capacitacion IdCapacitacionNavigation { get; set; }
        public virtual Persona IdPersonaNavigation { get; set; }
    }
}
