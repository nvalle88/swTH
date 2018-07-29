using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class EvaluacionCompetenciasTecnicasPuesto
    {
        public int IdEvaluacionCompetenciasTecnicasPuesto { get; set; }
        public int? IdNivelDesarrollo { get; set; }
        public int? IdEval001 { get; set; }
        public int? IdComportamientoObservable { get; set; }

        public virtual ComportamientoObservable IdComportamientoObservableNavigation { get; set; }
        public virtual Eval001 IdEval001Navigation { get; set; }
        public virtual NivelDesarrollo IdNivelDesarrolloNavigation { get; set; }
    }
}
