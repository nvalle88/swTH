using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class EvaluacionConocimientoFactor
    {
        public int IdEvaluacionConocimientoFactor { get; set; }
        public int IdFactor { get; set; }
        public int? IdEvaluacionConocimiento { get; set; }

        public virtual EvaluacionConocimiento IdEvaluacionConocimientoNavigation { get; set; }
        public virtual Factor IdFactorNavigation { get; set; }
    }
}
