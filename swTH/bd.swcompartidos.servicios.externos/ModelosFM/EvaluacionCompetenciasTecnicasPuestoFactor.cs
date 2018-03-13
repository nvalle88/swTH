using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class EvaluacionCompetenciasTecnicasPuestoFactor
    {
        public int IdEvaluacionCompetenciasTecnicasPuestoFactor { get; set; }
        public int IdFactor { get; set; }
        public int? IdEvaluacionCompetenciasTecnicasPuesto { get; set; }

        public virtual EvaluacionCompetenciasTecnicasPuesto IdEvaluacionCompetenciasTecnicasPuestoNavigation { get; set; }
        public virtual Factor IdFactorNavigation { get; set; }
    }
}
