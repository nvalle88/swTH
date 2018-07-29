using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class EvaluacionEvento
    {
        public EvaluacionEvento()
        {
            DetalleEvaluacionEvento = new HashSet<DetalleEvaluacionEvento>();
        }

        public int IdEvaluacionEvento { get; set; }
        public int? IdPlanCapacitacion { get; set; }
        public string Sugerencias { get; set; }

        public virtual ICollection<DetalleEvaluacionEvento> DetalleEvaluacionEvento { get; set; }
        public virtual PlanCapacitacion IdPlanCapacitacionNavigation { get; set; }
    }
}
