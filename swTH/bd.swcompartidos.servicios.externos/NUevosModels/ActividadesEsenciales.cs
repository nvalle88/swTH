using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class ActividadesEsenciales
    {
        public ActividadesEsenciales()
        {
            IndiceOcupacionalActividadesEsenciales = new HashSet<IndiceOcupacionalActividadesEsenciales>();
        }

        public int IdActividadesEsenciales { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<IndiceOcupacionalActividadesEsenciales> IndiceOcupacionalActividadesEsenciales { get; set; }
    }
}
