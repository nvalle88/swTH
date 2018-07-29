using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class GestionPlanCapacitacion
    {
        public GestionPlanCapacitacion()
        {
            PlanCapacitacion = new HashSet<PlanCapacitacion>();
        }

        public int IdGestionPlanCapacitacion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? Anio { get; set; }
        public int? Estado { get; set; }

        public virtual ICollection<PlanCapacitacion> PlanCapacitacion { get; set; }
    }
}
