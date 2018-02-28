using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class GrupoOcupacional
    {
        public GrupoOcupacional()
        {
            EscalaGrados = new HashSet<EscalaGrados>();
        }

        public int IdGrupoOcupacional { get; set; }
        public string TipoEscala { get; set; }

        public virtual ICollection<EscalaGrados> EscalaGrados { get; set; }
    }
}
