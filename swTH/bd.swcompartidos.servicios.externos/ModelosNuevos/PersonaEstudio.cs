using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class PersonaEstudio
    {
        public int IdPersonaEstudio { get; set; }
        public DateTime? FechaGraduado { get; set; }
        public string Observaciones { get; set; }
        public int IdTitulo { get; set; }
        public int IdPersona { get; set; }
        public string NoSenescyt { get; set; }

        public virtual Persona IdPersonaNavigation { get; set; }
    }
}
