using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
{
    public partial class IndiceOcupacionalCapacitaciones
    {
        public int IdIndiceOcupacionalCapacitaciones { get; set; }
        public int IdIndiceOcupacional { get; set; }
        public int IdCapacitacion { get; set; }

        public virtual Capacitacion IdCapacitacionNavigation { get; set; }
        public virtual IndiceOcupacional IdIndiceOcupacionalNavigation { get; set; }
    }
}
