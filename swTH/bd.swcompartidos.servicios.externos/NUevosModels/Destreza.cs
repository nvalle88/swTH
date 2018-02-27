using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class Destreza
    {
        public Destreza()
        {
            EvaluacionCompetenciasTecnicasPuestoDetalle = new HashSet<EvaluacionCompetenciasTecnicasPuestoDetalle>();
            EvaluacionCompetenciasUniversalesDetalle = new HashSet<EvaluacionCompetenciasUniversalesDetalle>();
        }

        public int IdDestreza { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<EvaluacionCompetenciasTecnicasPuestoDetalle> EvaluacionCompetenciasTecnicasPuestoDetalle { get; set; }
        public virtual ICollection<EvaluacionCompetenciasUniversalesDetalle> EvaluacionCompetenciasUniversalesDetalle { get; set; }
    }
}
