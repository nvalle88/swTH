using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class EvaluacionActividadesPuestoTrabajo
    {
        public EvaluacionActividadesPuestoTrabajo()
        {
            Eval001 = new HashSet<Eval001>();
            EvaluacionActividadesPuestoTrabajoDetalle = new HashSet<EvaluacionActividadesPuestoTrabajoDetalle>();
            EvaluacionActividadesPuestoTrabajoFactor = new HashSet<EvaluacionActividadesPuestoTrabajoFactor>();
        }

        public int IdEvaluacionActividadesPuestoTrabajo { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Eval001> Eval001 { get; set; }
        public virtual ICollection<EvaluacionActividadesPuestoTrabajoDetalle> EvaluacionActividadesPuestoTrabajoDetalle { get; set; }
        public virtual ICollection<EvaluacionActividadesPuestoTrabajoFactor> EvaluacionActividadesPuestoTrabajoFactor { get; set; }
    }
}
