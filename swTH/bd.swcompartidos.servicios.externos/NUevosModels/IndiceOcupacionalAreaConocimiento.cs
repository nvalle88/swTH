using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class IndiceOcupacionalAreaConocimiento
    {
        public int IdIndiceOcupacionalAreaConocimiento { get; set; }
        public int IdAreaConocimiento { get; set; }
        public int IdIndiceOcupacional { get; set; }

        public virtual AreaConocimiento IdAreaConocimientoNavigation { get; set; }
        public virtual IndiceOcupacional IdIndiceOcupacionalNavigation { get; set; }
    }
}
