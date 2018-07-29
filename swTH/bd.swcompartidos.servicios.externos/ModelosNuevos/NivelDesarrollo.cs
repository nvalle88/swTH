using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class NivelDesarrollo
    {
        public NivelDesarrollo()
        {
            EvaluacionCompetenciasTecnicasPuesto = new HashSet<EvaluacionCompetenciasTecnicasPuesto>();
        }

        public int IdNivelDesarrollo { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<EvaluacionCompetenciasTecnicasPuesto> EvaluacionCompetenciasTecnicasPuesto { get; set; }
    }
}
