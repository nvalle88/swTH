using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class EvaluacionActividadesPuestoTrabajoFactor
    {
        public int IdEvaluacionActividadesPuestoTrabajoFactor { get; set; }
        public int? IdFactor { get; set; }
        public int? IdEvaluacionActividadesPuestoTrabajo { get; set; }

        public virtual EvaluacionActividadesPuestoTrabajo IdEvaluacionActividadesPuestoTrabajoNavigation { get; set; }
        public virtual Factor IdFactorNavigation { get; set; }
    }
}
