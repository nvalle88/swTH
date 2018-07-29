using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class InventarioActivoFijo
    {
        public InventarioActivoFijo()
        {
            InventarioActivoFijoDetalle = new HashSet<InventarioActivoFijoDetalle>();
        }

        public int IdInventarioActivoFijo { get; set; }
        public DateTime FechaCorteInventario { get; set; }
        public DateTime FechaInforme { get; set; }
        public string NumeroInforme { get; set; }
        public int IdEstado { get; set; }
        public bool InventarioManual { get; set; }

        public virtual ICollection<InventarioActivoFijoDetalle> InventarioActivoFijoDetalle { get; set; }
        public virtual Estado IdEstadoNavigation { get; set; }
    }
}
