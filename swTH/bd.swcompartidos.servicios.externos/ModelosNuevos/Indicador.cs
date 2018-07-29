using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class Indicador
    {
        public Indicador()
        {
            EvaluacionActividadesPuestoTrabajo = new HashSet<EvaluacionActividadesPuestoTrabajo>();
        }

        public int IdIndicador { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<EvaluacionActividadesPuestoTrabajo> EvaluacionActividadesPuestoTrabajo { get; set; }
    }
}
