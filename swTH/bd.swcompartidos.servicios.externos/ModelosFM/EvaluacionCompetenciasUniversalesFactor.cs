using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class EvaluacionCompetenciasUniversalesFactor
    {
        public int IdEvaluacionCompetenciasUniversalesFactor { get; set; }
        public int IdFactor { get; set; }
        public int? IdEvaluacionCompetenciasUniversales { get; set; }

        public virtual EvaluacionCompetenciasUniversales IdEvaluacionCompetenciasUniversalesNavigation { get; set; }
        public virtual Factor IdFactorNavigation { get; set; }
    }
}
