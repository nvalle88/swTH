using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle
    {
        public int IdEvaluacionTrabajoEquipoIniciativaLiderazgoDetalle { get; set; }
        public int? IdEvaluacionTrabajoEquipoIniciativaLiderazgo { get; set; }
        public int? IdTrabajoEquipoIniciativaLiderazgo { get; set; }
        public int? IdRelevancia { get; set; }
        public int? IdFrecuenciaAplicacion { get; set; }

        public virtual EvaluacionTrabajoEquipoIniciativaLiderazgo IdEvaluacionTrabajoEquipoIniciativaLiderazgoNavigation { get; set; }
        public virtual FrecuenciaAplicacion IdFrecuenciaAplicacionNavigation { get; set; }
        public virtual Relevancia IdRelevanciaNavigation { get; set; }
        public virtual TrabajoEquipoIniciativaLiderazgo IdTrabajoEquipoIniciativaLiderazgoNavigation { get; set; }
    }
}
