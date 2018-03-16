using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class EvaluacionConocimientoDetalle
    {
        public int IdEvaluacionConocimientoDetalle { get; set; }
        public int IdEvaluacionConocimiento { get; set; }
        public int? IdNivelConocimiento { get; set; }

        public virtual EvaluacionConocimiento IdEvaluacionConocimientoNavigation { get; set; }
        public virtual NivelConocimiento IdNivelConocimientoNavigation { get; set; }
    }
}
