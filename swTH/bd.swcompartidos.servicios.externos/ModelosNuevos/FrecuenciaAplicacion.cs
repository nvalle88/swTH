using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class FrecuenciaAplicacion
    {
        public FrecuenciaAplicacion()
        {
            EvaluacionCompetenciasUniversales = new HashSet<EvaluacionCompetenciasUniversales>();
            EvaluacionTrabajoEquipoIniciativaLiderazgo = new HashSet<EvaluacionTrabajoEquipoIniciativaLiderazgo>();
        }

        public int IdFrecuenciaAplicacion { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<EvaluacionCompetenciasUniversales> EvaluacionCompetenciasUniversales { get; set; }
        public virtual ICollection<EvaluacionTrabajoEquipoIniciativaLiderazgo> EvaluacionTrabajoEquipoIniciativaLiderazgo { get; set; }
    }
}
