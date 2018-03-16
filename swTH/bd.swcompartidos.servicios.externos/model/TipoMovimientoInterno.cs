using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class TipoMovimientoInterno
    {
        public TipoMovimientoInterno()
        {
            EmpleadoMovimiento = new HashSet<EmpleadoMovimiento>();
        }

        public int IdTipoMovimientoInterno { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<EmpleadoMovimiento> EmpleadoMovimiento { get; set; }
    }
}
