using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class EvaluacionTrabajoEquipoIniciativaLiderazgo
    {
        public EvaluacionTrabajoEquipoIniciativaLiderazgo()
        {
            Eval001 = new HashSet<Eval001>();
            EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle = new HashSet<EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle>();
            EvaluacionTrabajoEquipoIniciativaLiderazgoFactor = new HashSet<EvaluacionTrabajoEquipoIniciativaLiderazgoFactor>();
        }

        public int IdEvaluacionTrabajoEquipoIniciativaLiderazgo { get; set; }
        public string Nombre { get; set; }
        public string ObservacionesJefeInmediato { get; set; }

        public virtual ICollection<Eval001> Eval001 { get; set; }
        public virtual ICollection<EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle> EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle { get; set; }
        public virtual ICollection<EvaluacionTrabajoEquipoIniciativaLiderazgoFactor> EvaluacionTrabajoEquipoIniciativaLiderazgoFactor { get; set; }
    }
}
