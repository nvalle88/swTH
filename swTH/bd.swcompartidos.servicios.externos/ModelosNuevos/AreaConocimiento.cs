using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosNuevos
{
    public partial class AreaConocimiento
    {
        public AreaConocimiento()
        {
            EvaluacionConocimiento = new HashSet<EvaluacionConocimiento>();
            IndiceOcupacionalAreaConocimiento = new HashSet<IndiceOcupacionalAreaConocimiento>();
            Titulo = new HashSet<Titulo>();
        }

        public int IdAreaConocimiento { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<EvaluacionConocimiento> EvaluacionConocimiento { get; set; }
        public virtual ICollection<IndiceOcupacionalAreaConocimiento> IndiceOcupacionalAreaConocimiento { get; set; }
        public virtual ICollection<Titulo> Titulo { get; set; }
    }
}
