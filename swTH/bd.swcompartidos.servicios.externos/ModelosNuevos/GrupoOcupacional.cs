using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class GrupoOcupacional
    {
        public GrupoOcupacional()
        {
            EscalaGrados = new HashSet<EscalaGrados>();
            SituacionPropuesta = new HashSet<SituacionPropuesta>();
        }

        public int IdGrupoOcupacional { get; set; }
        public string TipoEscala { get; set; }

        public virtual ICollection<EscalaGrados> EscalaGrados { get; set; }
        public virtual ICollection<SituacionPropuesta> SituacionPropuesta { get; set; }
    }
}
