using System;
using System.Collections.Generic;

namespace bd.swth.entidades.Negocio
{
    public partial class OtroIngreso
    {
        public int IdOtroIngreso { get; set; }
        public int IdDeclaracionPatrimonioPersonal { get; set; }
        public decimal? IngresoConyuge { get; set; }
        public decimal? IngresoArriendos { get; set; }
        public decimal? IngresoNegocioParticular { get; set; }
        public decimal? IngresoRentasFinancieras { get; set; }
        public decimal? OtrosIngresos { get; set; }
        public string DescripcionOtros { get; set; }
        public decimal Total { get; set; }

        public virtual DeclaracionPatrimonioPersonal IdDeclaracionPatrimonioPersonalNavigation { get; set; }
    }
}
