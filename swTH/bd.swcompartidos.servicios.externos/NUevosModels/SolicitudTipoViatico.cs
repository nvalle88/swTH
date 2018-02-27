using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class SolicitudTipoViatico
    {
        public int IdSolicitudTipoViatico { get; set; }
        public int IdTipoViatico { get; set; }
        public int IdSolicitudViatico { get; set; }

        public virtual SolicitudViatico IdSolicitudViaticoNavigation { get; set; }
        public virtual TipoViatico IdTipoViaticoNavigation { get; set; }
    }
}
