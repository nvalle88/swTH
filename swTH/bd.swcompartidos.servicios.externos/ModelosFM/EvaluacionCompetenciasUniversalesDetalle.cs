using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class EvaluacionCompetenciasUniversalesDetalle
    {
        public int IdEvaluacionCompetenciasUniversalesDetalle { get; set; }
        public int IdEvaluacionCompetenciasUniversales { get; set; }
        public int? IdDestreza { get; set; }
        public int? IdRelevancia { get; set; }
        public int? IdFrecuenciaAplicacion { get; set; }

        public virtual Destreza IdDestrezaNavigation { get; set; }
        public virtual EvaluacionCompetenciasUniversales IdEvaluacionCompetenciasUniversalesNavigation { get; set; }
        public virtual FrecuenciaAplicacion IdFrecuenciaAplicacionNavigation { get; set; }
        public virtual Relevancia IdRelevanciaNavigation { get; set; }
    }
}
