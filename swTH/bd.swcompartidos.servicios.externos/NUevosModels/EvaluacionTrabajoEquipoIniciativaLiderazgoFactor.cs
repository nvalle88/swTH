using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class EvaluacionTrabajoEquipoIniciativaLiderazgoFactor
    {
        public int IdEvaluacionTrabajoEquipoIniciativaLiderazgoFactor { get; set; }
        public int IdFactor { get; set; }
        public int? IdEvaluacionTrabajoEquipoIniciativaLiderazgo { get; set; }

        public virtual EvaluacionTrabajoEquipoIniciativaLiderazgo IdEvaluacionTrabajoEquipoIniciativaLiderazgoNavigation { get; set; }
        public virtual Factor IdFactorNavigation { get; set; }
    }
}
