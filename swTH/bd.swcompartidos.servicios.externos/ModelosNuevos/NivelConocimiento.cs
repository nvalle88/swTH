using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class NivelConocimiento
    {
        public NivelConocimiento()
        {
            EvaluacionConocimiento = new HashSet<EvaluacionConocimiento>();
        }

        public int IdNivelConocimiento { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<EvaluacionConocimiento> EvaluacionConocimiento { get; set; }
    }
}
