using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class TrabajoEquipoIniciativaLiderazgo
    {
        public TrabajoEquipoIniciativaLiderazgo()
        {
            EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle = new HashSet<EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle>();
        }

        public int IdTrabajoEquipoIniciativaLiderazgo { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle> EvaluacionTrabajoEquipoIniciativaLiderazgoDetalle { get; set; }
    }
}
