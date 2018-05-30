namespace bd.swth.entidades.Negocio
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class EvaluacionTrabajoEquipoIniciativaLiderazgo
    {
        [Key]
        public int IdEvaluacionTrabajoEquipoIniciativaLiderazgo { get; set; }
        public int? IdComportamientoObservable { get; set; }
        public int? IdFrecuenciaAplicacion { get; set; }
        public int? IdEval001 { get; set; }

        public virtual ComportamientoObservable ComportamientoObservable { get; set; }
        public virtual Eval001 Eval001 { get; set; }
        public virtual FrecuenciaAplicacion FrecuenciaAplicacion { get; set; }
    }
}
