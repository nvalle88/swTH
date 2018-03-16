using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class Factor
    {
        public Factor()
        {
            EvaluacionActividadesPuestoTrabajoFactor = new HashSet<EvaluacionActividadesPuestoTrabajoFactor>();
            EvaluacionCompetenciasTecnicasPuestoFactor = new HashSet<EvaluacionCompetenciasTecnicasPuestoFactor>();
            EvaluacionCompetenciasUniversalesFactor = new HashSet<EvaluacionCompetenciasUniversalesFactor>();
            EvaluacionConocimientoFactor = new HashSet<EvaluacionConocimientoFactor>();
            EvaluacionTrabajoEquipoIniciativaLiderazgoFactor = new HashSet<EvaluacionTrabajoEquipoIniciativaLiderazgoFactor>();
        }

        public int IdFactor { get; set; }
        public decimal? Porciento { get; set; }

        public virtual ICollection<EvaluacionActividadesPuestoTrabajoFactor> EvaluacionActividadesPuestoTrabajoFactor { get; set; }
        public virtual ICollection<EvaluacionCompetenciasTecnicasPuestoFactor> EvaluacionCompetenciasTecnicasPuestoFactor { get; set; }
        public virtual ICollection<EvaluacionCompetenciasUniversalesFactor> EvaluacionCompetenciasUniversalesFactor { get; set; }
        public virtual ICollection<EvaluacionConocimientoFactor> EvaluacionConocimientoFactor { get; set; }
        public virtual ICollection<EvaluacionTrabajoEquipoIniciativaLiderazgoFactor> EvaluacionTrabajoEquipoIniciativaLiderazgoFactor { get; set; }
    }
}
