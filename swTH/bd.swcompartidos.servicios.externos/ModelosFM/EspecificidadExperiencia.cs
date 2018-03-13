using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class EspecificidadExperiencia
    {
        public EspecificidadExperiencia()
        {
            ExperienciaLaboralRequerida = new HashSet<ExperienciaLaboralRequerida>();
        }

        public int IdEspecificidadExperiencia { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<ExperienciaLaboralRequerida> ExperienciaLaboralRequerida { get; set; }
    }
}
