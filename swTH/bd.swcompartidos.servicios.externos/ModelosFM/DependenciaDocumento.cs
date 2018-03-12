using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class DependenciaDocumento
    {
        public int IdDependenciaDocumento { get; set; }
        public int DocumentosSubidos { get; set; }
        public int IdDependencia { get; set; }

        public virtual DocumentosCargados DocumentosSubidosNavigation { get; set; }
        public virtual Dependencia IdDependenciaNavigation { get; set; }
    }
}
