using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class FlujoAprobacion
    {
        public int IdFlujoAprobacion { get; set; }
        public int? IdTipoAccionPersonal { get; set; }
        public int? IdEmpleado { get; set; }

        public virtual TipoAccionPersonal IdTipoAccionPersonalNavigation { get; set; }
    }
}
