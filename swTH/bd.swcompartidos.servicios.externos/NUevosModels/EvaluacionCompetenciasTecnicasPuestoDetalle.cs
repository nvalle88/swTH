using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class EvaluacionCompetenciasTecnicasPuestoDetalle
    {
        public int IdEvaluacionCompetenciasTecnicasPuestoDetalle { get; set; }
        public int IdEvaluacionCompetenciasTecnicasPuesto { get; set; }
        public int? IdDestreza { get; set; }
        public int? IdRelevancia { get; set; }
        public int? IdNivelDesarrollo { get; set; }

        public virtual Destreza IdDestrezaNavigation { get; set; }
        public virtual EvaluacionCompetenciasTecnicasPuesto IdEvaluacionCompetenciasTecnicasPuestoNavigation { get; set; }
        public virtual NivelDesarrollo IdNivelDesarrolloNavigation { get; set; }
        public virtual Relevancia IdRelevanciaNavigation { get; set; }
    }
}
