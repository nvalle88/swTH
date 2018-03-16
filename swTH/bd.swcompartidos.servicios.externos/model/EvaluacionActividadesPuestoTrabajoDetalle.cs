using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class EvaluacionActividadesPuestoTrabajoDetalle
    {
        public int IdEvaluacionActividadesPuestoTrabajoDetalle { get; set; }
        public int IdEvaluacionActividadesPuestoTrabajo { get; set; }
        public int? IdIndicador { get; set; }
        public string DescripcionActividad { get; set; }
        public int MetaPeriodo { get; set; }
        public int ActividadesCumplidas { get; set; }

        public virtual EvaluacionActividadesPuestoTrabajo IdEvaluacionActividadesPuestoTrabajoNavigation { get; set; }
        public virtual Indicador IdIndicadorNavigation { get; set; }
    }
}
