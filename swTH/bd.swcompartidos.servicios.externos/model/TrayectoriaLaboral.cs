using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class TrayectoriaLaboral
    {
        public int IdTrayectoriaLaboral { get; set; }
        public int IdPersona { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Empresa { get; set; }
        public string PuestoTrabajo { get; set; }
        public string DescripcionFunciones { get; set; }

        public virtual Persona IdPersonaNavigation { get; set; }
    }
}
