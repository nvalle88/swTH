using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class GeneralCapacitacion
    {
        public GeneralCapacitacion()
        {
            PlanCapacitacionAmbitoCapacitacionNavigation = new HashSet<PlanCapacitacion>();
            PlanCapacitacionEstadoEventoNavigation = new HashSet<PlanCapacitacion>();
            PlanCapacitacionNombreEventoNavigation = new HashSet<PlanCapacitacion>();
            PlanCapacitacionTipoCapacitacionNavigation = new HashSet<PlanCapacitacion>();
            PlanCapacitacionTipoEvaluacionNavigation = new HashSet<PlanCapacitacion>();
            PlanCapacitacionTipoEventoNavigation = new HashSet<PlanCapacitacion>();
        }

        public int IdGeneralCapacitacion { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }

        public virtual ICollection<PlanCapacitacion> PlanCapacitacionAmbitoCapacitacionNavigation { get; set; }
        public virtual ICollection<PlanCapacitacion> PlanCapacitacionEstadoEventoNavigation { get; set; }
        public virtual ICollection<PlanCapacitacion> PlanCapacitacionNombreEventoNavigation { get; set; }
        public virtual ICollection<PlanCapacitacion> PlanCapacitacionTipoCapacitacionNavigation { get; set; }
        public virtual ICollection<PlanCapacitacion> PlanCapacitacionTipoEvaluacionNavigation { get; set; }
        public virtual ICollection<PlanCapacitacion> PlanCapacitacionTipoEventoNavigation { get; set; }
    }
}
