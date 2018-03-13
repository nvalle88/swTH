using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class Estudio
    {
        public Estudio()
        {
            ExperienciaLaboralRequerida = new HashSet<ExperienciaLaboralRequerida>();
            IndiceOcupacionalEstudio = new HashSet<IndiceOcupacionalEstudio>();
            Titulo = new HashSet<Titulo>();
        }

        public int IdEstudio { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<ExperienciaLaboralRequerida> ExperienciaLaboralRequerida { get; set; }
        public virtual ICollection<IndiceOcupacionalEstudio> IndiceOcupacionalEstudio { get; set; }
        public virtual ICollection<Titulo> Titulo { get; set; }
    }
}
