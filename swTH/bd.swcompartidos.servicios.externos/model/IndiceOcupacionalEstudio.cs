using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class IndiceOcupacionalEstudio
    {
        public int IdIndiceOcupacionalEstudio { get; set; }
        public int IdIndiceOcupacional { get; set; }
        public int IdEstudio { get; set; }

        public virtual Estudio IdEstudioNavigation { get; set; }
        public virtual IndiceOcupacional IdIndiceOcupacionalNavigation { get; set; }
    }
}
