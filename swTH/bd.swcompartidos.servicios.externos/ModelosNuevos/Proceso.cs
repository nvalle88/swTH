using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Proceso
    {
        public Proceso()
        {
            Dependencia = new HashSet<Dependencia>();
            SituacionPropuesta = new HashSet<SituacionPropuesta>();
        }

        public int IdProceso { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Dependencia> Dependencia { get; set; }
        public virtual ICollection<SituacionPropuesta> SituacionPropuesta { get; set; }
    }
}
