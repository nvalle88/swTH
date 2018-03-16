using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class EvaluacionConocimiento
    {
        public EvaluacionConocimiento()
        {
            Eval001 = new HashSet<Eval001>();
            EvaluacionConocimientoDetalle = new HashSet<EvaluacionConocimientoDetalle>();
            EvaluacionConocimientoFactor = new HashSet<EvaluacionConocimientoFactor>();
        }

        public int IdEvaluacionConocimiento { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Eval001> Eval001 { get; set; }
        public virtual ICollection<EvaluacionConocimientoDetalle> EvaluacionConocimientoDetalle { get; set; }
        public virtual ICollection<EvaluacionConocimientoFactor> EvaluacionConocimientoFactor { get; set; }
    }
}
