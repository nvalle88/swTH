using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class Indicador
    {
        public Indicador()
        {
            EvaluacionActividadesPuestoTrabajoDetalle = new HashSet<EvaluacionActividadesPuestoTrabajoDetalle>();
        }

        public int IdIndicador { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<EvaluacionActividadesPuestoTrabajoDetalle> EvaluacionActividadesPuestoTrabajoDetalle { get; set; }
    }
}
