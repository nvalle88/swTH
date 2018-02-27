using System;
using System.Collections.Generic;

namespace bd.sw.externos.NUevosModels
{
    public partial class AvanceGestionCambio
    {
        public int IdAvanceGestionCambio { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Indicadorreal { get; set; }
        public int IdActividadesGestionCambio { get; set; }

        public virtual ActividadesGestionCambio IdActividadesGestionCambioNavigation { get; set; }
    }
}
