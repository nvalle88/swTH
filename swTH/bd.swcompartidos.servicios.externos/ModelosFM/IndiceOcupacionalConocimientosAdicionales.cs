using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class IndiceOcupacionalConocimientosAdicionales
    {
        public int IdIndiceOcupacionalConocimientosAdicionales { get; set; }
        public int IdConocimientosAdicionales { get; set; }
        public int IdIndiceOcupacional { get; set; }

        public virtual ConocimientosAdicionales IdConocimientosAdicionalesNavigation { get; set; }
        public virtual IndiceOcupacional IdIndiceOcupacionalNavigation { get; set; }
    }
}
