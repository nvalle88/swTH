namespace bd.swth.entidades.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Eval001
    {
        [Key]
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
        public virtual ICollection<Quejas> Quejas { get; set; }
        public virtual ICollection<EvaluacionTrabajoEquipoIniciativaLiderazgo> EvaluacionTrabajoEquipoIniciativaLiderazgo { get; set; }
        public virtual Empleado EmpleadoEvaluado { get; set; }
        public virtual EscalaEvaluacionTotal EscalaEvaluacionTotal{ get; set; }
        public virtual Evaluador Evaluador { get; set; }

    }
}
