using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Eval001
    {
        public Eval001()
        {
            EvaluacionActividadesPuestoTrabajo = new HashSet<EvaluacionActividadesPuestoTrabajo>();
            EvaluacionCompetenciasTecnicasPuesto = new HashSet<EvaluacionCompetenciasTecnicasPuesto>();
            EvaluacionCompetenciasUniversales = new HashSet<EvaluacionCompetenciasUniversales>();
            EvaluacionConocimiento = new HashSet<EvaluacionConocimiento>();
            EvaluacionTrabajoEquipoIniciativaLiderazgo = new HashSet<EvaluacionTrabajoEquipoIniciativaLiderazgo>();
            Quejas = new HashSet<Quejas>();
        }

        public int IdEval001 { get; set; }
        public int? IdEmpleadoEvaluado { get; set; }
        public int? IdEscalaEvaluacionTotal { get; set; }
        public int IdEvaluador { get; set; }
        public bool? EstaConforme { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaEvaluacionDesde { get; set; }
        public DateTime FechaEvaluacionHasta { get; set; }
        public string Observaciones { get; set; }

        public virtual ICollection<EvaluacionActividadesPuestoTrabajo> EvaluacionActividadesPuestoTrabajo { get; set; }
        public virtual ICollection<EvaluacionCompetenciasTecnicasPuesto> EvaluacionCompetenciasTecnicasPuesto { get; set; }
        public virtual ICollection<EvaluacionCompetenciasUniversales> EvaluacionCompetenciasUniversales { get; set; }
        public virtual ICollection<EvaluacionConocimiento> EvaluacionConocimiento { get; set; }
        public virtual ICollection<EvaluacionTrabajoEquipoIniciativaLiderazgo> EvaluacionTrabajoEquipoIniciativaLiderazgo { get; set; }
        public virtual ICollection<Quejas> Quejas { get; set; }
        public virtual Empleado IdEmpleadoEvaluadoNavigation { get; set; }
        public virtual EscalaEvaluacionTotal IdEscalaEvaluacionTotalNavigation { get; set; }
        public virtual Evaluador IdEvaluadorNavigation { get; set; }
    }
}
