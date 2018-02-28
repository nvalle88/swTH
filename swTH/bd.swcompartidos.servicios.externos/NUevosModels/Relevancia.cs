using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class Relevancia
    {
        public Relevancia()
        {
            EvaluacionCompetenciasTecnicasPuestoDetalle = new HashSet<EvaluacionCompetenciasTecnicasPuestoDetalle>();
            EvaluacionCompetenciasUniversalesDetalle = new HashSet<EvaluacionCompetenciasUniversalesDetalle>();
            EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle = new HashSet<EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle>();
        }

        public int IdRelevancia { get; set; }
        public string Nombre { get; set; }
        public string ComportamientoObservable { get; set; }

        public virtual ICollection<EvaluacionCompetenciasTecnicasPuestoDetalle> EvaluacionCompetenciasTecnicasPuestoDetalle { get; set; }
        public virtual ICollection<EvaluacionCompetenciasUniversalesDetalle> EvaluacionCompetenciasUniversalesDetalle { get; set; }
        public virtual ICollection<EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle> EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle { get; set; }
    }
}
