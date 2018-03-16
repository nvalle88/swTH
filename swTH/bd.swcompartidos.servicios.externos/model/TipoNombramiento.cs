using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class TipoNombramiento
    {
        public TipoNombramiento()
        {
            IndiceOcupacionalModalidadPartida = new HashSet<IndiceOcupacionalModalidadPartida>();
        }

        public int IdTipoNombramiento { get; set; }
        public int IdRelacionLaboral { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<IndiceOcupacionalModalidadPartida> IndiceOcupacionalModalidadPartida { get; set; }
        public virtual RelacionLaboral IdRelacionLaboralNavigation { get; set; }
    }
}
