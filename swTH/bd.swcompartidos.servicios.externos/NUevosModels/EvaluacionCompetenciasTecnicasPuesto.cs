using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class EvaluacionCompetenciasTecnicasPuesto
    {
        public EvaluacionCompetenciasTecnicasPuesto()
        {
            Eval001 = new HashSet<Eval001>();
            EvaluacionCompetenciasTecnicasPuestoDetalle = new HashSet<EvaluacionCompetenciasTecnicasPuestoDetalle>();
            EvaluacionCompetenciasTecnicasPuestoFactor = new HashSet<EvaluacionCompetenciasTecnicasPuestoFactor>();
        }

        public int IdEvaluacionCompetenciasTecnicasPuesto { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Eval001> Eval001 { get; set; }
        public virtual ICollection<EvaluacionCompetenciasTecnicasPuestoDetalle> EvaluacionCompetenciasTecnicasPuestoDetalle { get; set; }
        public virtual ICollection<EvaluacionCompetenciasTecnicasPuestoFactor> EvaluacionCompetenciasTecnicasPuestoFactor { get; set; }
    }
}
