using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class MotivoAsiento
    {
        public int IdMotivoAsiento { get; set; }
        public string Descripcion { get; set; }
        public int IdConfiguracionContabilidad { get; set; }

        public virtual ConfiguracionContabilidad IdConfiguracionContabilidadNavigation { get; set; }
    }
}
