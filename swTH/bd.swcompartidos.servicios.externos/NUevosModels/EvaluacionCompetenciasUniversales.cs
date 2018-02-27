using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class EvaluacionCompetenciasUniversales
    {
        public EvaluacionCompetenciasUniversales()
        {
            Eval001 = new HashSet<Eval001>();
            EvaluacionCompetenciasUniversalesDetalle = new HashSet<EvaluacionCompetenciasUniversalesDetalle>();
            EvaluacionCompetenciasUniversalesFactor = new HashSet<EvaluacionCompetenciasUniversalesFactor>();
        }

        public int IdEvaluacionCompetenciasUniversales { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Eval001> Eval001 { get; set; }
        public virtual ICollection<EvaluacionCompetenciasUniversalesDetalle> EvaluacionCompetenciasUniversalesDetalle { get; set; }
        public virtual ICollection<EvaluacionCompetenciasUniversalesFactor> EvaluacionCompetenciasUniversalesFactor { get; set; }
    }
}
