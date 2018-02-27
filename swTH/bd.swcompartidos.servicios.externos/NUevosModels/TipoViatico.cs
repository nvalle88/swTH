using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class TipoViatico
    {
        public TipoViatico()
        {
            SolicitudTipoViatico = new HashSet<SolicitudTipoViatico>();
        }

        public int IdTipoViatico { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<SolicitudTipoViatico> SolicitudTipoViatico { get; set; }
    }
}
