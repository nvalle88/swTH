using System;
using System.Collections.Generic;

namespace bd.sw.externos.model
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
