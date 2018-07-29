using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class CapacitacionTemarioProveedor
    {
        public CapacitacionTemarioProveedor()
        {
            CapacitacionRecibida = new HashSet<CapacitacionRecibida>();
        }

        public int IdCapacitacionTemarioProveedor { get; set; }
        public int IdCapacitacionTemario { get; set; }
        public int IdCapacitacionProveedor { get; set; }
        public int? NumeroHoras { get; set; }
        public decimal Costo { get; set; }
        public int IdCapacitacionModalidad { get; set; }

        public virtual ICollection<CapacitacionRecibida> CapacitacionRecibida { get; set; }
        public virtual CapacitacionModalidad IdCapacitacionModalidadNavigation { get; set; }
        public virtual CapacitacionProveedor IdCapacitacionProveedorNavigation { get; set; }
        public virtual CapacitacionTemario IdCapacitacionTemarioNavigation { get; set; }
    }
}
