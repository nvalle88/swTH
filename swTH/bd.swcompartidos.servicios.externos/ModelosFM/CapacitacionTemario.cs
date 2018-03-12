using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class CapacitacionTemario
    {
        public CapacitacionTemario()
        {
            CapacitacionPlanificacion = new HashSet<CapacitacionPlanificacion>();
            CapacitacionTemarioProveedor = new HashSet<CapacitacionTemarioProveedor>();
        }

        public int IdCapacitacionTemario { get; set; }
        public int IdCapacitacionAreaConocimiento { get; set; }
        public string Tema { get; set; }

        public virtual ICollection<CapacitacionPlanificacion> CapacitacionPlanificacion { get; set; }
        public virtual ICollection<CapacitacionTemarioProveedor> CapacitacionTemarioProveedor { get; set; }
        public virtual CapacitacionAreaConocimiento IdCapacitacionAreaConocimientoNavigation { get; set; }
    }
}
