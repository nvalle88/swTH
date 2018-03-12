using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class CapacitacionModalidad
    {
        public CapacitacionModalidad()
        {
            CapacitacionPlanificacion = new HashSet<CapacitacionPlanificacion>();
            CapacitacionTemarioProveedor = new HashSet<CapacitacionTemarioProveedor>();
        }

        public int IdCapacitacionModalidad { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<CapacitacionPlanificacion> CapacitacionPlanificacion { get; set; }
        public virtual ICollection<CapacitacionTemarioProveedor> CapacitacionTemarioProveedor { get; set; }
    }
}
