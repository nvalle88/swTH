using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class Eval001
    {
        public int IdEval001 { get; set; }
        public int? IdEmpleadoEvaluado { get; set; }
        public int? IdEscalaEvaluacionTotal { get; set; }
        public int? IdEvaluacionActividadesPuestoTrabajo { get; set; }
        public int? IdEvaluacionConocimiento { get; set; }
        public int? IdEvaluacionCompetenciasTecnicasPuesto { get; set; }
        public int? IdEvaluacionCompetenciasUniversales { get; set; }
        public int? IdEvaluacionTrabajoEquipoIniciativaLiderazgo { get; set; }
        public int IdEvaluador { get; set; }
        public bool? EstaConforme { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaEvaluacionDesde { get; set; }
        public DateTime FechaEvaluacionHasta { get; set; }
        public string Observaciones { get; set; }

        public virtual EscalaEvaluacionTotal IdEscalaEvaluacionTotalNavigation { get; set; }
        public virtual EvaluacionActividadesPuestoTrabajo IdEvaluacionActividadesPuestoTrabajoNavigation { get; set; }
        public virtual EvaluacionCompetenciasTecnicasPuesto IdEvaluacionCompetenciasTecnicasPuestoNavigation { get; set; }
        public virtual EvaluacionCompetenciasUniversales IdEvaluacionCompetenciasUniversalesNavigation { get; set; }
        public virtual EvaluacionConocimiento IdEvaluacionConocimientoNavigation { get; set; }
        public virtual EvaluacionTrabajoEquipoIniciativaLiderazgo IdEvaluacionTrabajoEquipoIniciativaLiderazgoNavigation { get; set; }
        public virtual Evaluador IdEvaluadorNavigation { get; set; }
    }
}
