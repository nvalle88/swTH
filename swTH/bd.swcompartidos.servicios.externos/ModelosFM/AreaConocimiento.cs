using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class AreaConocimiento
    {
        public AreaConocimiento()
        {
            IndiceOcupacionalAreaConocimiento = new HashSet<IndiceOcupacionalAreaConocimiento>();
            Titulo = new HashSet<Titulo>();
        }

        public int IdAreaConocimiento { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<IndiceOcupacionalAreaConocimiento> IndiceOcupacionalAreaConocimiento { get; set; }
        public virtual ICollection<Titulo> Titulo { get; set; }
    }
}
