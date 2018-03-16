using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class FrecuenciaAplicacion
    {
        public FrecuenciaAplicacion()
        {
            EvaluacionCompetenciasUniversalesDetalle = new HashSet<EvaluacionCompetenciasUniversalesDetalle>();
            EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle = new HashSet<EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle>();
        }

        public int IdFrecuenciaAplicacion { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<EvaluacionCompetenciasUniversalesDetalle> EvaluacionCompetenciasUniversalesDetalle { get; set; }
        public virtual ICollection<EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle> EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle { get; set; }
    }
}
