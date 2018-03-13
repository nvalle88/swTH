using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class EmpleadoMovimiento
    {
        public int IdEmpleadoMovimiento { get; set; }
        public int IdTipoMovimientoInterno { get; set; }
        public int IdIndiceOcupacionalModalidadPartida { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public int IdEmpleado { get; set; }

        public virtual IndiceOcupacionalModalidadPartida IdIndiceOcupacionalModalidadPartidaNavigation { get; set; }
        public virtual TipoMovimientoInterno IdTipoMovimientoInternoNavigation { get; set; }
    }
}
