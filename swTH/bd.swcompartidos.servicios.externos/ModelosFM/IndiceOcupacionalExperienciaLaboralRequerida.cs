using System;
using System.Collections.Generic;

namespace bd.sw.externos.ModelosFM
{
    public partial class IndiceOcupacionalExperienciaLaboralRequerida
    {
        public int IdIndiceOcupacionalExperienciaLaboralRequerida { get; set; }
        public int? IdIndiceOcupacional { get; set; }
        public int? IdExperienciaLaboralRequerida { get; set; }

        public virtual ExperienciaLaboralRequerida IdExperienciaLaboralRequeridaNavigation { get; set; }
        public virtual IndiceOcupacional IdIndiceOcupacionalNavigation { get; set; }
    }
}
