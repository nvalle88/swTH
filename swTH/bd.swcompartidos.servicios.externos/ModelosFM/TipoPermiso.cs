using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class TipoPermiso
    {
        public TipoPermiso()
        {
            SolicitudPermiso = new HashSet<SolicitudPermiso>();
        }

        public int IdTipoPermiso { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<SolicitudPermiso> SolicitudPermiso { get; set; }
    }
}
