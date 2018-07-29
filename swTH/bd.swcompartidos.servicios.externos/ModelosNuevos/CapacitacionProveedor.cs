using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class CapacitacionProveedor
    {
        public CapacitacionProveedor()
        {
            CapacitacionTemarioProveedor = new HashSet<CapacitacionTemarioProveedor>();
            PlanCapacitacion = new HashSet<PlanCapacitacion>();
        }

        public int IdCapacitacionProveedor { get; set; }
        public string Telefono { get; set; }
        public string Nombre { get; set; }
        public int? IdPais { get; set; }
        public string Direccion { get; set; }
        public string Especialidad { get; set; }

        public virtual ICollection<CapacitacionTemarioProveedor> CapacitacionTemarioProveedor { get; set; }
        public virtual ICollection<PlanCapacitacion> PlanCapacitacion { get; set; }
        public virtual Pais IdPaisNavigation { get; set; }
    }
}
