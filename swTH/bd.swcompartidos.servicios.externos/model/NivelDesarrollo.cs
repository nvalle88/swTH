using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class NivelDesarrollo
    {
        public NivelDesarrollo()
        {
            EvaluacionCompetenciasTecnicasPuestoDetalle = new HashSet<EvaluacionCompetenciasTecnicasPuestoDetalle>();
        }

        public int IdNivelDesarrollo { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<EvaluacionCompetenciasTecnicasPuestoDetalle> EvaluacionCompetenciasTecnicasPuestoDetalle { get; set; }
    }
}
