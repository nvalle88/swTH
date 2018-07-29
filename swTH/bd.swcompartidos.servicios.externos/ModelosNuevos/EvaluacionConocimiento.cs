using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class EvaluacionConocimiento
    {
        public int IdEvaluacionConocimiento { get; set; }
        public int? IdNivelConocimiento { get; set; }
        public int? IdEval001 { get; set; }
        public int? IdAreaConocimiento { get; set; }

        public virtual AreaConocimiento IdAreaConocimientoNavigation { get; set; }
        public virtual Eval001 IdEval001Navigation { get; set; }
        public virtual NivelConocimiento IdNivelConocimientoNavigation { get; set; }
    }
}
