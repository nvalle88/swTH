using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class ExperienciaLaboralRequerida
    {
        public ExperienciaLaboralRequerida()
        {
            IndiceOcupacionalExperienciaLaboralRequerida = new HashSet<IndiceOcupacionalExperienciaLaboralRequerida>();
        }

        public int IdExperienciaLaboralRequerida { get; set; }
        public int IdEspecificidadExperiencia { get; set; }
        public int AnoExperiencia { get; set; }
        public int MesExperiencia { get; set; }
        public int IdEstudio { get; set; }

        public virtual ICollection<IndiceOcupacionalExperienciaLaboralRequerida> IndiceOcupacionalExperienciaLaboralRequerida { get; set; }
        public virtual EspecificidadExperiencia IdEspecificidadExperienciaNavigation { get; set; }
        public virtual Estudio IdEstudioNavigation { get; set; }
    }
}
