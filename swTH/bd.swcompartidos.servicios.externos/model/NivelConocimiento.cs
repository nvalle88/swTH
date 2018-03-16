using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class NivelConocimiento
    {
        public NivelConocimiento()
        {
            EvaluacionConocimientoDetalle = new HashSet<EvaluacionConocimientoDetalle>();
        }

        public int IdNivelConocimiento { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<EvaluacionConocimientoDetalle> EvaluacionConocimientoDetalle { get; set; }
    }
}
