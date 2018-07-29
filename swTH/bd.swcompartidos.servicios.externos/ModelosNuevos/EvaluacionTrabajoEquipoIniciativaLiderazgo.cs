using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class EvaluacionTrabajoEquipoIniciativaLiderazgo
    {
        public int IdEvaluacionTrabajoEquipoIniciativaLiderazgo { get; set; }
        public int? IdComportamientoObservable { get; set; }
        public int? IdFrecuenciaAplicacion { get; set; }
        public int? IdEval001 { get; set; }

        public virtual ComportamientoObservable IdComportamientoObservableNavigation { get; set; }
        public virtual Eval001 IdEval001Navigation { get; set; }
        public virtual FrecuenciaAplicacion IdFrecuenciaAplicacionNavigation { get; set; }
    }
}
