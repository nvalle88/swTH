using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class IndiceOcupacionalActividadesEsenciales
    {
        public int IdIndiceOcupacionalActividadesEsenciales { get; set; }
        public int IdActividadesEsenciales { get; set; }
        public int IdIndiceOcupacional { get; set; }

        public virtual ActividadesEsenciales IdActividadesEsencialesNavigation { get; set; }
        public virtual IndiceOcupacional IdIndiceOcupacionalNavigation { get; set; }
    }
}
